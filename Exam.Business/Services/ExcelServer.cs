using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Exam.Business.Services
{
    public static class ExcelServer
    {
        public static  string UploadExcel(this IFormFile file, IWebHostEnvironment env, string folder)
        {
            string filename = "exam"+ Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(env.WebRootPath, folder, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyTo(stream);
            }

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        { 
                           
                        }
                    } while (reader.NextResult());
                }
            }

            return filename;
        }
    }
}
