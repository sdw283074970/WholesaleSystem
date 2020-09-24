using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WholesaleSystem.Models;

namespace WholesaleSystem.Manager
{
    public class UploadManager
    {
        private ApplicationDbContext _context;

        public UploadManager()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<ImageFile> AutoParseImages(List<IFormFile> files)
        {
            if (files.Count < 1)
            {
                throw new Exception("文件为空");
            }
            //返回的文件地址
            var imageFiles = new List<ImageFile>();
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format("Images/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
            //获取当前web目录
            var rootPath = @"D://UploadedFiles/";
            if (!Directory.Exists(rootPath + filePath))
            {
                Directory.CreateDirectory(rootPath + filePath);
            }
            try
            {
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        #region  图片文件的条件判断
                        //文件后缀
                        var fileExtension = Path.GetExtension(item.FileName);

                        //判断后缀是否是图片
                        const string fileFilt = ".gif|.jpg|.jpeg|.png";
                        if (fileExtension == null)
                        {
                            throw new Exception("上传的文件没有后缀");
                            //return Error("上传的文件没有后缀");
                        }
                        if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                        {
                            throw new Exception("请上传jpg、png、gif格式的图片");
                            //return Error("请上传jpg、png、gif格式的图片");
                        }

                        //判断文件大小    
                        long length = item.Length;
                        if (length > 1024 * 1024 * 4) //4M
                        {
                            throw new Exception("上传的文件不能大于4M");
                            //return Error("上传的文件不能大于4M");
                        }

                        #endregion

                        var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                        var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                        var saveName = item.FileName.Split('.')[0] + "_" + strRan + fileExtension;

                        //插入图片数据
                        using (FileStream fs = File.Create(rootPath + filePath + saveName))
                        {
                            // 解析文件名称，看是否符合规则
                            var productInventoryInDb = GetSingleOrDefaultProductInventory(item.FileName.Split('.')[0]);

                            if (productInventoryInDb == null)
                                throw new Exception("Upload failed. SKU: " + item.FileName.Split('.')[0].Split('_')[0] + " was not found in inventory.Please upload all the files of this batch again");

                            imageFiles.Add(new ImageFile
                            {
                                Active = true,
                                IsMainPicture = productInventoryInDb.ImageFiles == null ? true : (productInventoryInDb.ImageFiles.SingleOrDefault(x => x.IsMainPicture == true) == null ? true : false),
                                Url = filePath + saveName,
                                Path = rootPath + filePath + saveName,
                                UploadBy = "N/A",
                                UploadDate = DateTime.Now,
                                PictureName = item.FileName,
                                ProductInventory = productInventoryInDb
                            });

                            item.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }

                _context.ImageFiles.AddRange(imageFiles);
                _context.SaveChanges();

                return imageFiles;
            }
            catch (Exception ex)
            {
                //这边增加日志，记录错误的原因
                //ex.ToString();
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ImageFile> UploadImagesToProduct(int productInventoryId, List<IFormFile> files)
        {
            if (files.Count < 1)
            {
                throw new Exception("文件为空");
            }
            //返回的文件地址
            var imageFiles = new List<ImageFile>();
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format("Images/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
            //获取当前web目录
            var rootPath = @"D://UploadedFiles/";

            var productInventoryInDb = _context.ProductInventories.Find(productInventoryId);

            if (!Directory.Exists(rootPath + filePath))
            {
                Directory.CreateDirectory(rootPath + filePath);
            }
            try
            {
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        #region  图片文件的条件判断
                        //文件后缀
                        var fileExtension = Path.GetExtension(item.FileName);

                        //判断后缀是否是图片
                        const string fileFilt = ".gif|.jpg|.jpeg|.png";
                        if (fileExtension == null)
                        {
                            throw new Exception("上传的文件没有后缀");
                            //return Error("上传的文件没有后缀");
                        }
                        if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                        {
                            throw new Exception("请上传jpg、png、gif格式的图片");
                            //return Error("请上传jpg、png、gif格式的图片");
                        }

                        //判断文件大小    
                        long length = item.Length;
                        if (length > 1024 * 1024 * 4) //4M
                        {
                            throw new Exception("上传的文件不能大于4M");
                            //return Error("上传的文件不能大于4M");
                        }

                        #endregion

                        var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                        var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                        var saveName = item.FileName.Split('.')[0] + "_" + strRan + fileExtension;

                        //插入图片数据
                        using (FileStream fs = File.Create(rootPath + filePath + saveName))
                        {
                            // 解析文件名称，看是否符合规则
                            if (productInventoryInDb == null)
                                throw new Exception("Upload failed. SKU: " + item.FileName.Split('.')[0].Split('_')[0] + " was not found in inventory.Please upload all the files of this batch again");

                            imageFiles.Add(new ImageFile
                            {
                                Active = true,
                                IsMainPicture = productInventoryInDb.ImageFiles == null ? true : (productInventoryInDb.ImageFiles.SingleOrDefault(x => x.IsMainPicture == true) == null ? true : false),
                                Url = filePath + saveName,
                                Path = rootPath + filePath + saveName,
                                UploadBy = "N/A",
                                UploadDate = DateTime.Now,
                                PictureName = item.FileName,
                                ProductInventory = productInventoryInDb
                            });

                            item.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }

                _context.ImageFiles.AddRange(imageFiles);
                _context.SaveChanges();

                return imageFiles;
            }
            catch (Exception ex)
            {
                //这边增加日志，记录错误的原因
                //ex.ToString();
                throw new Exception(ex.Message);
            }
        }

        public ProductInventory GetSingleOrDefaultProductInventory(string fileNameWithoutExtention)
        {
            var sku = fileNameWithoutExtention.Split('_')[0];

            var productInventoryInDb = _context.ProductInventories
                .Include(x => x.ImageFiles)
                .SingleOrDefault(x => x.Product_sku == sku);

            return productInventoryInDb;
        }
    }
}
