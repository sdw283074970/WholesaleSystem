using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using WholesaleSystem.Models;

namespace WholesaleSystem.Manager
{
    public class UploadManager
    {
        public IEnumerable<PicturePath> UploadPicFile(List<IFormFile> files)
        {
            if (files.Count < 1)
            {
                throw new Exception("文件为空");
            }
            //返回的文件地址
            var filePaths = new List<PicturePath>();
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format("/Uploads/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
            //获取当前web目录
            var rootPath = @"D://pic";
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
                        var saveName = strDateTime + strRan + fileExtension;

                        //插入图片数据                 
                        using (FileStream fs = System.IO.File.Create(rootPath + filePath + saveName))
                        {
                            item.CopyTo(fs);
                            fs.Flush();
                        }

                        // TO DO
                        filePaths.Add(new PicturePath { 
                            Active = true,
                            IsMainPicture = false,
                            Url = "Img/" + saveName,
                            Path = filePath + saveName,
                            UploadBy = "N/A",
                            UploadDate = DateTime.Now,
                            PictureName = item.FileName
                        });
                    }
                }

                return filePaths;
            }
            catch (Exception ex)
            {
                //这边增加日志，记录错误的原因
                //ex.ToString();
                throw new Exception("上传失败");
            }
        }
    }
}
