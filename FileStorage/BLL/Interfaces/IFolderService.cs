using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IFolderService
    {
        public bool CreateFolder(string directory);

        public bool DeleteFolder(string directory);


    }
}
