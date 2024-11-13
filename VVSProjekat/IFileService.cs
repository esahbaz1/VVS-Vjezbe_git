using System;
using System.Collections.Generic;

namespace VVSProjekat
{
    public interface IFileService
    {
        void SaveToFile(List<Task> tasks, string fileType);
        void DeleteCompletedTask(Task task);
        List<Task> LoadFromFile(string fileType);
        
    }

}
