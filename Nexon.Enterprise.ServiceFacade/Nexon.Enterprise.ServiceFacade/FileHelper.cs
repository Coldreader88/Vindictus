using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;

namespace Nexon.Enterprise.ServiceFacade
{
	public static class FileHelper
	{
		public static void Copy(string sourceDirectory, string targetDirectory)
		{
			DirectoryInfo source = new DirectoryInfo(sourceDirectory);
			DirectoryInfo target = new DirectoryInfo(targetDirectory);
			FileHelper.CopyAll(source, target);
		}

		public static void Move(string sourceDirectory, string targetDirectory)
		{
			DirectoryInfo source = new DirectoryInfo(sourceDirectory);
			DirectoryInfo target = new DirectoryInfo(targetDirectory);
			FileHelper.CopyAll(source, target);
		}

		private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
		{
			if (!Directory.Exists(target.FullName))
			{
				Directory.CreateDirectory(target.FullName);
			}
			foreach (FileInfo fileInfo in source.GetFiles())
			{
				Console.WriteLine("Copying {0}\\{1}", target.FullName, fileInfo.Name);
				fileInfo.CopyTo(Path.Combine(target.ToString(), fileInfo.Name), true);
			}
			foreach (DirectoryInfo directoryInfo in source.GetDirectories())
			{
				DirectoryInfo target2 = target.CreateSubdirectory(directoryInfo.Name);
				FileHelper.CopyAll(directoryInfo, target2);
			}
		}

		private static void MoveAll(DirectoryInfo source, DirectoryInfo target)
		{
			if (!Directory.Exists(target.FullName))
			{
				Directory.CreateDirectory(target.FullName);
			}
			foreach (FileInfo fileInfo in source.GetFiles())
			{
				Console.WriteLine("Copying {0}\\{1}", target.FullName, fileInfo.Name);
				fileInfo.MoveTo(Path.Combine(target.ToString(), fileInfo.Name));
			}
			foreach (DirectoryInfo directoryInfo in source.GetDirectories())
			{
				DirectoryInfo target2 = target.CreateSubdirectory(directoryInfo.Name);
				FileHelper.MoveAll(directoryInfo, target2);
			}
		}

		public static string GetComputeHash(string path)
		{
			string result;
			using (FileStream fileStream = File.OpenRead(path))
			{
				result = BitConverter.ToString(FileHelper.md5Provider.ComputeHash(fileStream));
			}
			return result;
		}

		public static void ZipFiles(string inputFolderPath, string outputFolderPath, string outputFileName, string password, bool isCreated)
		{
			LiveUpdateCollectionEntity liveUpdateCollectionEntity = new LiveUpdateCollectionEntity();
			List<string> list = FileHelper.GenerateFileList(inputFolderPath);
			int num = Directory.GetParent(inputFolderPath).ToString().Length;
			num++;
			FileStream fileStream = null;
			ZipOutputStream zipOutputStream = null;
			string path = Path.Combine(outputFolderPath, outputFileName);
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (!isCreated && File.Exists(path))
			{
				return;
			}
			try
			{
				zipOutputStream = new ZipOutputStream(File.Create(path));
				if (!string.IsNullOrEmpty(password))
				{
					zipOutputStream.Password = password;
				}
				zipOutputStream.SetLevel(9);
				foreach (string text in list)
				{
					ZipEntry zipEntry = new ZipEntry(text.Remove(0, num));
					zipOutputStream.PutNextEntry(zipEntry);
					if (!text.EndsWith("/"))
					{
						fileStream = File.OpenRead(text);
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						zipOutputStream.Write(array, 0, array.Length);
						liveUpdateCollectionEntity.Data.Add(new LiveUpdateEntity(text, BitConverter.ToString(FileHelper.md5Provider.ComputeHash(fileStream))));
					}
				}
				string text2 = Path.Combine(inputFolderPath, "checksum.xml");
				if (FileHelper.CreateCheckSumFile(liveUpdateCollectionEntity, text2))
				{
					ZipEntry zipEntry = new ZipEntry("checksum.xml");
					zipOutputStream.PutNextEntry(zipEntry);
					fileStream = File.OpenRead(text2);
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, array.Length);
					zipOutputStream.Write(array, 0, array.Length);
					string path2 = Path.Combine(directoryName, "checksum.xml");
					if (File.Exists(path2))
					{
						File.Delete(path2);
					}
					File.Copy(text2, Path.Combine(directoryName, "checksum.xml"));
				}
				zipOutputStream.Finish();
				zipOutputStream.Close();
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			finally
			{
				if (zipOutputStream != null)
				{
					zipOutputStream.Dispose();
				}
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
		}

		private static bool CreateCheckSumFile(LiveUpdateCollectionEntity entity, string path)
		{
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				try
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(LiveUpdateCollectionEntity));
					dataContractSerializer.WriteObject(fileStream, entity);
					result = true;
				}
				catch
				{
					result = false;
				}
				finally
				{
					fileStream.Close();
				}
			}
			return result;
		}

		internal static List<string> GenerateFileList(string Dir)
		{
			List<string> list = new List<string>();
			bool flag = true;
			foreach (string item in Directory.GetFiles(Dir))
			{
				list.Add(item);
				flag = false;
			}
			if (flag && Directory.GetDirectories(Dir).Length == 0)
			{
				list.Add(Dir + "/");
			}
			foreach (string dir in Directory.GetDirectories(Dir))
			{
				foreach (string item2 in FileHelper.GenerateFileList(dir))
				{
					list.Add(item2);
				}
			}
			return list;
		}

		public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
		{
			ZipInputStream zipInputStream = null;
			FileStream fileStream = null;
			try
			{
				zipInputStream = new ZipInputStream(File.OpenRead(zipPathAndFile));
				if (!string.IsNullOrEmpty(password))
				{
					zipInputStream.Password = password;
				}
				string empty = string.Empty;
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					string fileName = Path.GetFileName(nextEntry.Name);
					if (!string.IsNullOrEmpty(outputFolder) && !Directory.Exists(outputFolder))
					{
						Directory.CreateDirectory(outputFolder);
					}
					if (fileName != string.Empty && nextEntry.Name.IndexOf(".ini") < 0)
					{
						string text = outputFolder + "\\" + nextEntry.Name;
						text = text.Replace("\\ ", "\\");
						string directoryName = Path.GetDirectoryName(text);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						fileStream = File.Create(text);
						byte[] array = new byte[2048];
						for (;;)
						{
							int num = zipInputStream.Read(array, 0, array.Length);
							if (num <= 0)
							{
								break;
							}
							fileStream.Write(array, 0, num);
						}
						fileStream.Close();
					}
				}
				zipInputStream.Close();
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			finally
			{
				if (zipInputStream != null)
				{
					zipInputStream.Dispose();
				}
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
			if (deleteZipFile)
			{
				try
				{
					File.Delete(zipPathAndFile);
				}
				catch
				{
				}
			}
		}

		private static MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
	}
}
