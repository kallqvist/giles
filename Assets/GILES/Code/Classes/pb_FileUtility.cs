﻿using UnityEngine;
using System.IO;

namespace GILES
{
	/**
	 * Helper functions for reading information from files.
	 */
	public static class pb_FileUtility
	{
		/**
		 * Read all text from a file path.
		 */
		public static string ReadFile(string path)
		{
			if( !File.Exists(path))
			{
				Debug.LogError("File path does not exist!\n" + path);
				return "";
			}

			string contents = File.ReadAllText(path);

			return contents;
		}

		/**
		 * Save some text to a file path.  Does not check that folder structure is valid!
		 */
		public static bool SaveFile(string path, string contents)
		{
			try
			{
				File.WriteAllText(path, contents);
			} 
			catch(System.Exception e)
			{
				Debug.LogError("Failed writing to path: " + path + "\n" + e.ToString());
				return false;
			}

			return true;
		}

		public static bool IsValidPath(string path, string extension)
		{
			return !string.IsNullOrEmpty(path) && path.EndsWith(extension);
		}

		/**
		 * Given a string, this function attempts to extrapolate the absolute path using current directory as the
		 * relative root.
		 */
		public static string GetFullPath(string path)
		{
			return Path.GetFullPath(path);
        }

		/**
		 * Return the path type (file or directory)
		 */
		public static PathType GetPathType(string path)
		{
			return File.Exists(path) ? PathType.File : (Directory.Exists(path) ? PathType.Directory : PathType.Null);
		}

		/**
		 * Replace backslashes with forward slashes, and make sure that path is the full path.
		 */
		public static string SanitizePath(string path, string extension = "")
		{
            return GetFullPath(Path.Combine(path, extension));
		}
	}
}
