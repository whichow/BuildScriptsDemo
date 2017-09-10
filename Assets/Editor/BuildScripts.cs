using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class BuildScripts {

	[MenuItem("Assets/BuildScripts")]
	static void Build()
	{
		string[] paths = _SearchCSScripts();
		FileStream fs = new FileStream("build.txt", FileMode.Create);
		StreamWriter writer = new StreamWriter(fs);
		// writer.WriteLine("-target:library -sdk:2 -out:my.dll");
		writer.WriteLine("-debug -target:library -nowarn:0169 -langversion:4 -out:Temp/BuildScripts.dll -unsafe -optimize -r:\"C:/Program Files/Unity 5.6.2f1/Editor/Data/PlaybackEngines/windowsstandalonesupport/Managed/UnityEngine.dll\" -r:\"C:/Program Files/Unity 5.6.2f1/Editor/Data/UnityExtensions/Unity/GUISystem/Standalone/UnityEngine.UI.dll\" -r:\"C:/Program Files/Unity 5.6.2f1/Editor/Data/UnityExtensions/Unity/Networking/Standalone/UnityEngine.Networking.dll\"");
		foreach(var path in paths)
		{
			writer.WriteLine(path);
		}
		writer.Flush();
		writer.Close();
		fs.Close();
		Process p = new Process();
		ProcessStartInfo info = new ProcessStartInfo();
		info.FileName = @"C:\Program Files\Unity 5.6.2f1\Editor\Data\MonoBleedingEdge\lib\mono\4.5\mcs.exe";
		info.Arguments = "@build.txt";
		info.UseShellExecute = false;
		info.RedirectStandardOutput = true;
		info.RedirectStandardError = true;
		p.StartInfo = info;
		p.Start();
		Debug.Log(p.StandardOutput.ReadToEnd());
		Debug.LogError(p.StandardError.ReadToEnd());
	}

	private static string[] _SearchCSScripts()
	{
		string path = Application.dataPath + "/HttpClient";
		return Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
	}
}
