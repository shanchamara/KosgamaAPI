using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using System.IO.Compression;

namespace CommonStockManagementServices.Services
{
    public class SaveImages
    {
        public static object SetImageUrl(object model, IEnumerable<IFormFile> images, string[] imageProperties, string subFolderName, string defaultImage = "Select.png")
        {

            string baseFolderPath = CommonResources.System_File_Path;
            // Combine base path and subfolder
            var folderPath = Path.Combine(baseFolderPath, subFolderName);



            // Get images as a list to ensure proper indexing
            var imageList = images.ToList();

            for (int i = 0; i < imageProperties.Length; i++)
            {
                // Get the property name dynamically based on index
                var propertyName = imageProperties[i];

                // Get the current image property value
                var currentImageUrl = model.GetType().GetProperty(propertyName)?.GetValue(model)?.ToString();

                // Delete existing image if necessary
                if (!string.IsNullOrEmpty(currentImageUrl) && currentImageUrl != defaultImage)
                {
                    var currentImagePath = Path.Combine(folderPath, currentImageUrl);
                    if (File.Exists(currentImagePath))
                    {
                        File.Delete(currentImagePath);
                    }
                }

                // Check if the corresponding image exists in the list
                if (i < imageList.Count && imageList[i] != null && imageList[i].Length > 0)
                {
                    var imageFile = imageList[i];

                    // Save new image with unique name
                    var fileExtension = Path.GetExtension(imageFile.FileName);
                    var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var newFilePath = Path.Combine(folderPath, newFileName);

                    using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    // Set the new file name to the appropriate property
                    model.GetType().GetProperty(propertyName)?.SetValue(model, newFileName);
                }
                else
                {
                    // Set default image if no image is provided
                    model.GetType().GetProperty(propertyName)?.SetValue(model, defaultImage);
                }
            }
            return model;

        }


        public static object SetExcelFile(object model, IFormFile excelFile, string fileProperty, string subFolderName, string defaultFile = "Default")
        {
            string baseFolderPath = CommonResources.System_File_Path;
            // Combine base path and subfolder
            var folderPath = Path.Combine(baseFolderPath, subFolderName);

            // Get the current file property value
            var currentFileUrl = model.GetType().GetProperty(fileProperty)?.GetValue(model)?.ToString();

            // Delete existing file if necessary
            if (!string.IsNullOrEmpty(currentFileUrl) && currentFileUrl != defaultFile)
            {
                var currentFilePath = Path.Combine(folderPath, currentFileUrl);
                if (File.Exists(currentFilePath))
                {
                    File.Delete(currentFilePath);
                }
            }

            // Check if a valid file is provided
            if (excelFile != null && excelFile.Length > 0)
            {
                // Save new file with a unique name
                var fileExtension = Path.GetExtension(excelFile.FileName);
                if (fileExtension != ".xlsx" && fileExtension != ".xls")
                {
                    throw new InvalidOperationException("Invalid file format. Please upload a valid Excel file.");
                }

                var newFileName = $"{defaultFile}{Guid.NewGuid()}{fileExtension}";
                var newFilePath = Path.Combine(folderPath, newFileName);

                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    excelFile.CopyTo(fileStream);
                }

                // Set the new file name to the appropriate property
                model.GetType().GetProperty(fileProperty)?.SetValue(model, newFileName);
            }
            else
            {
                // Set default file if no file is provided
                model.GetType().GetProperty(fileProperty)?.SetValue(model, defaultFile);
            }

            return model;
        }


        public void DeleteImage(string ImageURl, string filepath)
        {
            if (ImageURl != "Select.png" && ImageURl != null)
            {
                var imagePath = Path.Combine(CommonResources.System_File_Path, filepath, ImageURl);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

        public static void DeleteExcel(string fileUrl, string filePath)
        {
            // Check if the file URL is valid and not the default file
            if (fileUrl != "Default.xlsx" && !string.IsNullOrEmpty(fileUrl))
            {
                // Combine the base folder path with the provided file path and file URL
                var excelFilePath = Path.Combine(CommonResources.System_File_Path, filePath, fileUrl);

                // Check if the file exists and delete it
                if (File.Exists(excelFilePath))
                {
                    File.Delete(excelFilePath);
                }
            }
        }
        public static FileResult GetExcelFile(string fileName, string subFolderName)
        {
            string baseFolderPath = CommonResources.System_File_Path;
            var folderPath = Path.Combine(baseFolderPath, subFolderName);
            var filePath = Path.Combine(folderPath);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = filePath
            };
        }


        #region UploadZipFiles

        public List<string> ExtractImagesFromZip(string zipFilePath, string folderPath)
        {
            string extractPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(zipFilePath));

            Directory.CreateDirectory(extractPath);

            List<string> imageFiles = new();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

            using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    string fileExtension = Path.GetExtension(entry.FullName).ToLower();
                    if (!string.IsNullOrEmpty(fileExtension) && allowedExtensions.Contains(fileExtension))
                    {
                        string destinationPath = Path.Combine(extractPath, entry.Name);
                        entry.ExtractToFile(destinationPath, overwrite: true);
                        imageFiles.Add(destinationPath);
                    }
                }
            }

            return imageFiles;
        }

        #endregion
    }
}
