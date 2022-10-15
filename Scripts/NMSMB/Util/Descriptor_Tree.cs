//=============================================================================
// Starting with a TkModelDescriptorList (top-level for *.DESCRIPTOR.MBIN items)
// recurse through tree, extracting referenced mbin's as needed.
//=============================================================================

public partial class ModelDescriptorListNode
{
	public ResourceDescriptorDataNode       Parent   = null;
	public NMS.PAK.MBIN.Data                MbinData = null;
	public TkModelDescriptorList            MbinObj  = null;
	public List<ResourceDescriptorListNode> Children = new();
	public int                              Depth    => Parent == null ? 0 : 1 + Parent.Depth;
}

//=============================================================================

public partial class ResourceDescriptorListNode
{
	public ModelDescriptorListNode          Parent   = null;
	public NMS.PAK.MBIN.Data                MbinData = null;
	public TkResourceDescriptorList         MbinObj  = null;
	public List<ResourceDescriptorDataNode> Children = new();
	public int                              Depth    => Parent == null ? 0 : 1 + Parent.Depth;
}

//=============================================================================

public partial class ResourceDescriptorDataNode
{
	public ResourceDescriptorListNode    Parent   = null;
	public NMS.PAK.MBIN.Data             MbinData = null;
	public TkResourceDescriptorData      MbinObj  = null;
	public List<ModelDescriptorListNode> Children = new();
	public int                           Depth    => Parent == null ? 0 : 1 + Parent.Depth;
}

//=============================================================================

public partial class ModelDescriptorTree
{
	public ModelDescriptorTree(
		NMS.PAK.Item.ICollection COLLECTION,
		NMS.PAK.Item.Path        PATH  // *.DESCRIPTOR.MBIN (TkModelDescriptorList) path
	){
		IPakItemCollection = COLLECTION;
		Root = BuildBranch(null, PATH);
	}

	//...........................................................

	public readonly NMS.PAK.Item.ICollection              IPakItemCollection;
	public readonly Dictionary<string, NMS.PAK.MBIN.Data> DataCache = new();
	public readonly ModelDescriptorListNode               Root;

	//...........................................................

	protected ModelDescriptorListNode BuildBranch(
		ResourceDescriptorDataNode PARENT,
		NMS.PAK.Item.Path          PATH
	){
		if( PATH.IsNullOrEmpty() ) return null;

		NMS.PAK.MBIN.Data data = null;
		if( !DataCache.TryGetValue(PATH, out data) ) {
			data = IPakItemCollection?.ExtractData<NMS.PAK.MBIN.Data>(PATH);
			DataCache.Add(PATH, data);
		}
		var mdl = data?.ModObjectAs<TkModelDescriptorList>();
		return BuildBranch(PARENT, data, mdl);
	}

	//...........................................................

	protected ModelDescriptorListNode BuildBranch(
		ResourceDescriptorDataNode PARENT,
		NMS.PAK.MBIN.Data          MBIN_DATA,
		TkModelDescriptorList      MDL
	){
		if( MBIN_DATA == null || MDL == null ) return null;
		var node = new ModelDescriptorListNode(){
			Parent   = PARENT,
			MbinData = MBIN_DATA,
			MbinObj  = MDL
		};
		foreach( var rdl in MDL.List ) {
			var branch  = BuildBranch(node, rdl);
			if( branch != null ) node.Children.Add(branch);
		}
		return node;
	}

	//...........................................................

	protected ResourceDescriptorListNode BuildBranch(
		ModelDescriptorListNode  PARENT,
		TkResourceDescriptorList RDL
	){
		if( RDL == null ) return null;
		var node = new ResourceDescriptorListNode(){
			Parent   = PARENT,
			MbinData = PARENT.MbinData,
			MbinObj  = RDL
		};
		foreach( var rdd in RDL.Descriptors ) {
			var branch  = BuildBranch(node, rdd);
			if( branch != null ) node.Children.Add(branch);
		}
		return node;
	}

	//...........................................................

	protected ResourceDescriptorDataNode BuildBranch(
		ResourceDescriptorListNode PARENT,
		TkResourceDescriptorData   RDD
	){
		if( RDD == null ) return null;
		var node = new ResourceDescriptorDataNode(){
			Parent   = PARENT,
			MbinData = PARENT.MbinData,
			MbinObj  = RDD
		};
		foreach( var obj in RDD.Children ) {
			if( obj is TkModelDescriptorList mdl ) {
				var branch  = BuildBranch(node, node.MbinData, mdl);
				if( branch != null ) node.Children.Add(branch);
			}
		}
		foreach( var ref_path in RDD.ReferencePaths ) {
			var path    = ref_path.Value.Replace(".SCENE.", ".DESCRIPTOR.");
			var branch  = BuildBranch(node, path);
			if( branch != null ) node.Children.Add(branch);
		}
		return node;
	}
}

//=============================================================================
