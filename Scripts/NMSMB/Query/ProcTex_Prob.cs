//=============================================================================
// Used by: NMS Creative & Sharing Hub discord server.
// Export all probabilities of procedural textures.
//=============================================================================

public class Proc_Tex_Prob : cmk.NMS.Script.QueryClass
{
	protected class Texture {
		public string                         Path;
		public List<TkProceduralTextureLayer> Layers = new();
	}
	
	protected class Material {
		public string            Path;
		public TkMaterialSampler Diffuse;  // gDiffuseMap
		public Texture           Texture;  // Diffuse.Map, replace ".DDS" with ".TEXTURE.DDS", null if doesn't exist
	}
	
	protected class Node {
		public Node       Parent;
		public string     MbinPath;  // mbin path
		public string     NodePath => string.Join('/', Parent?.NodePath, Name);
		public string     Name;
		public Material   Material;
		public List<Node> Children = new();
	}

	protected struct Scene {
		public string Name;
		public string Path;
	}	
	protected Scene [] m_scenes = new Scene [] {
		new(){ Name = "BIOSHIP",    Path = "MODELS/COMMON/SPACECRAFT/S-CLASS/BIOPARTS/BIOSHIP_PROC.SCENE.MBIN" },
		new(){ Name = "DROPSHIP",   Path = "MODELS/COMMON/SPACECRAFT/DROPSHIPS/DROPSHIP_PROC.SCENE.MBIN" },
		new(){ Name = "FIGHTER",    Path = "MODELS/COMMON/SPACECRAFT/FIGHTERS/FIGHTER_PROC.SCENE.MBIN" },
		new(){ Name = "S-CLASS",    Path = "MODELS/COMMON/SPACECRAFT/S-CLASS/S-CLASS_PROC.SCENE.MBIN" },
		new(){ Name = "SAILSHIP",   Path = "MODELS/COMMON/SPACECRAFT/SAILSHIP/SAILSHIP_PROC.SCENE.MBIN" },
		new(){ Name = "SCIENTIFIC", Path = "MODELS/COMMON/SPACECRAFT/SCIENTIFIC/SCIENTIFIC_PROC.SCENE.MBIN" },
		new(){ Name = "SHUTTLE",    Path = "MODELS/COMMON/SPACECRAFT/SHUTTLE/SHUTTLE_PROC.SCENE.MBIN" }
	};
	
	//...........................................................
	
	protected override void Execute()
	{
		Log.Clear();

		var dir = Dialog.SelectFolder();
		if( dir.IsNullOrEmpty() ) return;
		
		foreach( var scene in m_scenes ) {
			var root = BuildBranch(scene.Path, null);
			ExportTree(root, System.IO.Path.Join(dir, scene.Name + ".txt"));
		//	ExportLua (root, System.IO.Path.Join(dir, scene.Name + ".lua"));
		}
		
		Log.AddSuccess("Finished");
	}
	
	//...........................................................

	protected Node BuildBranch( string SCENE_PATH, Node PARENT )
	{
		var mbin  = ExtractMbin<TkSceneNodeData>(SCENE_PATH, true, PARENT == null);			
		var node  = BuildBranch(mbin, SCENE_PATH, PARENT);
		//if( node != null ) node.Name = null;
		return node;
	}
	
	//...........................................................

	protected Node BuildBranch( TkSceneNodeData DATA, string SCENE_PATH, Node PARENT )
	{
		if( Cancel.IsCancellationRequested ) return null;
		
		var node = new Node(){ Parent = PARENT, MbinPath = SCENE_PATH, Name = DATA.Name };
		if( node.Name.IsNullOrEmpty() ) node.Name = "null";
		
		var attr  = DATA.Attributes.Find(ATTR => ATTR.Name == "MATERIAL");
		if( attr != null ) {
			node.Material = GetMaterial(attr.Value);
		}
	
		attr = DATA.Attributes.Find(ATTR => ATTR.Name == "SCENEGRAPH");
		if( attr != null ) {
			var branch  = BuildBranch(attr.Value, node);
			if( branch != null ) node.Children.Add(branch);
		}
		
		foreach( var child in DATA.Children ) {
			var branch  = BuildBranch(child, SCENE_PATH, node);
			if( branch != null ) node.Children.Add(branch);
		}

		if( node.Material == null &&
			node.Children.IsNullOrEmpty()
		)	return null;
		
		return node;
	}
	
	//...........................................................

	protected Material GetMaterial( string MATERIAL_PATH )
	{
		var material = new Material(){ Path = MATERIAL_PATH };
		var mbin     = ExtractMbin<TkMaterialData>(MATERIAL_PATH, true, false);
		
		material.Diffuse = mbin.Samplers.Find(SAMPLER => SAMPLER.Name == "gDiffuseMap");
		if( material.Diffuse == null ) return null;
		
		var texture_path = new NMS.PAK.Item.Path(material.Diffuse.Map);
		texture_path.Extension = ".TEXTURE.MBIN";
		
		var texture_mbin  = ExtractMbin<TkProceduralTextureList>(texture_path, true, false);  // may not exist
		if( texture_mbin == null ) return null;
		
		material.Texture = new Texture(){ Path = texture_path };
		foreach( var layer in texture_mbin.Layers ) {
			if( layer.Textures.IsNullOrEmpty() ) continue;
			material.Texture.Layers.Add(layer);
		}

		if( material.Texture.Layers.IsNullOrEmpty()
		)	return null;
		
		return material;
	}	

	//...........................................................

	protected void ExportTree( Node ROOT, string PATH )
	{
		var builder = new StringBuilder();
		ExportBranch(ROOT, "", builder);
		cmk.IO.File.WriteAllText(PATH, builder.ToString(), Log, Cancel);
	}

	protected void ExportBranch( Node NODE, string INDENT, StringBuilder BUILDER )
	{
		BUILDER.AppendLine($"{INDENT}{NODE.Name} ");
		INDENT += "  ";
		
		if( NODE.Material != null ) {
			foreach( var layer in NODE.Material.Texture.Layers ) {
				BUILDER.AppendLine($"{INDENT}{layer.Name.Value} {layer.Probability}");
				foreach( var texture in layer.Textures ) {
					BUILDER.AppendLine($"{INDENT}  {texture.Name.Value} {texture.Probability} {texture.Palette.Palette}");
				}
			}
		}
		
		foreach( var child in NODE.Children ) {
			ExportBranch(child, INDENT, BUILDER);
		}
	}
	
	//...........................................................

	protected void ExportLua( Node ROOT, string PATH )
	{
		var builder = new StringBuilder();
		ExportLua(ROOT, "", builder);
		cmk.IO.File.WriteAllText(PATH, builder.ToString(), Log, Cancel);
	}

	protected void ExportLua( Node NODE, string INDENT, StringBuilder BUILDER )
	{
		
	}
}

//=============================================================================
