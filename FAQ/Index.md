# FAQ

Q: What are all those mod scripts, can I get rid of them ?<br/>
A: They are functional samples that illustrate a variety of common modding tasks.  You can either delete the files|folders from the ./Scripts/Mod/ folder, deselect them when Executing the compiled mod scripts, or programmatically disable them in the (Configure) script.

Q: Why do some scripts have '(' ')' around them ?</br>
A: It's a convention the app uses to identify required scripts.  These are scripts that should not be deleted and are always included|executed.  In particular, the Utility Script (Global) is included in all Query and Mod script compiles, it is where new using statements should go; the Mod Script (Configure) is where you should add code to enable|disable other mod scripts, you can do this to disable all the included scripts when Executed.

Q: Why do some mod scripts have '[', ']' around them ?</br>
A: It's a convention the app uses to identify play-as scripts.  These are scripts that can be executed by other mod scripts, but are not displayed or executed by default in the Build tab.  They are intended as extensions to the (Configure) mod script.

Q: What does play-as mean ?</br>
A: It's an idea to encapsulate all mod's for a given play style e.g. play-as explorer, play-as salvager, ... .  Each would customize how the other scripts would Execute in order to support some vision of your player 'class'.
