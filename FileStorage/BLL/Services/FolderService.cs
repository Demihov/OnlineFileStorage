using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLL.Interfaces;

namespace BLL.Services
{
    public class FolderService: IFolderService
    {
        public bool CreateFolder(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    return true;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return false;
            }

            return false;
        }

        public bool DeleteFolder(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
                //log
                return true;
            }
            catch (Exception ex)
            {
                //log
            }

            return false;
        }
    }
}
