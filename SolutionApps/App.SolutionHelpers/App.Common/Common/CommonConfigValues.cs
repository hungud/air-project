using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace App.Common
{
    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Description:For get App Configuration Settings Data.
    /// </summary>
    public class ConfigValues
    {
        public static string TempPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationManager.AppSettings["TempUploadPath1"]);
            }
        }
        public static string ConnectionString
        {
            get{
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CONNECTION_STRING"]);
            }
        }
        public static string FileUpload
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["APPFileUploadFolder"]);
            }
        }
        public static string FileUploadTemp
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["APPTempUploadPath"]);
            }
        }
        public static string FileUploadFinal
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["APPFinalUploadPath"]);
            }
        }

        public static string GalleryFilePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["PHOTO_GALLERY"]);
            }
        }
        
        public static string StorageFilePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["STORAGE_FILE_PATH"]);
            }
        }

        public static string StorageFileVirtualPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["STORAGE_FILE_VIRTUAL_PATH"]);
            }
        }

        public static string ApplicationDefaultImage
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["APPLICATION_DEFAULT_IMAGE"]);
            }
        }

        public static Int32 TagCloudKeywordCount
        {
            get
            {
                return AppConvert.ToInt32(ConfigurationSettings.AppSettings["TAG_CLOUD_KEYWORD_COUNT"]);
            }
        }

        public static Int32 MostViewDataSetCount
        {
            get
            {
                return AppConvert.ToInt32(ConfigurationSettings.AppSettings["MOST_VIEWED_DATASET_COUNT"]);
            }
        }

        public static Int32 HomeAppCount
        {
            get
            {
                return AppConvert.ToInt32(ConfigurationSettings.AppSettings["HOME_APP_COUNT"]);
            }
        }


        public static Int64 MaxApplicationImageSize
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["MAX_APPLICATION_IMAGE_SIZE"]);
            }
        }

        public static Int64 ApplicationImageHeight
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["APPLICATION_IMAGE_HEIGHT"]);
            }
        }

        public static Int64 ApplicationImageWidth
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["APPLICATION_IMAGE_WIDTH"]);
            }
        }

        public static Int64 MaxCarouselPhotoImageSize
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["MAX_CAROUSEL_PHOTO_IMAGE_SIZE"]);
            }
        }

        public static Int64 CarouselPhotoImageHeight
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["CAROUSEL_PHOTO_IMAGE_HEIGHT"]);
            }
        }

        public static Int64 CarouselPhotoImageWidth
        {
            get
            {
                return AppConvert.ToLong(ConfigurationSettings.AppSettings["CAROUSEL_PHOTO_IMAGE_WIDTH"]);
            }
        }

        public static string AdminSiteVirtualPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["ADMIN_SITE_VIRTUAL_PATH"]);
            }
        }

        public static string PublicSiteVirtualPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["PUBLIC_SITE_VIRTUAL_PATH"]);
            }
        }

        public static string CatlogDirectory
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["CATLOG_DIR"]), ConfigValues.CurrentDirName);
            }
        }

        public static string SGDataFolderPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SG_DATA_FOLDER_PATH"]);
            }
        }

        public static string MetaKeywordsDependancyFile
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["META_KEYWORDS_DEPENDANCY_FILE"]), ConfigValues.CurrentDirName);
            }
        }

        public static string MetaDataFile
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["META_DATA_FILE"]), ConfigValues.CurrentDirName);
            }
        }

        public static string MetaDataDBFilePath
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["META_DATA_DB_FILE_PATH"]), ConfigValues.CurrentDirName);
            }
        }

        public static string MetaDataXMLFilePath
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["META_DATA_XML_FILE_PATH"]), ConfigValues.CurrentDirName);
            }
        }

        public static string SQLiteConnectionString
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["SQLITE_CONNECTION_STRING"]), ConfigValues.CurrentDirName);
            }
        }

        public static string AbbrevationsFile
        {
            get
            {
                return String.Format(AppConvert.ToString(ConfigurationSettings.AppSettings["ABBREVATIONS_FILE"]), ConfigValues.CurrentDirName);
            }
        }

        public static string LuceneIndexPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["LUCENE_INDEX_PATH"]);
            }
        }

        public static string KeywordXMLPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["KEYWORD_XML_PATH"]);
            }
        }

        public static string RequiredFilesPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["REQUIRED_FILES_PATH"]);
            }
        }

        public static string CurrentDirPath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CURRENT_DIR_PATH"]);
            }
        }

        public static string SQLiteFile
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SQLITE_FILE"]);
            }
        }

        public static string CurrentDirFilePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CURRENT_DIR_FILE_PATH"]);
            }
        }

        public static string BatchUserName
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["BATCH_USER_NAME"]);
            }
        }

        public static string BatchUserPassword
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["BATCH_USER_PASSWORD"]);
            }
        }

        public static string MailToList
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["MAIL_TO_LIST"]);
            }
        }

        public static string MailFromList
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["MAIL_FROM_LIST"]);
            }
        }

        public static string MailCCList
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CC_LIST"]);
            }
        }

        public static string MailSubject
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["MAIL_SUBJECT"]);
            }
        }

        public static string MailFooter
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["MAIL_BODY_END_TEXT"]);
            }
        }

        public static string MailSMTPClient
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["MAIL_SMTP_CLIENT"]);
            }
        }

        public static string OneMapKey
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["ONEMAP_KEY"]);
            }
        }

        public static string MetaDataDownloadFileURL
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["METADATA_DOWNLOAD_FILE_URL"]);
            }
        }

        public static string AgenciesCategories
        {
            get
            {
                return String.Format(ConfigurationSettings.AppSettings["AGENCIES_CATEGORIES_XML"], ConfigValues.CurrentDirName);
            }
        }

        public static string SMTPServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SMTP_SERVER"]);
            }
        }

        public static string SMTPUid
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SMTP_UID"]);
            }
        }

        public static string SMTPPass
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SMTP_PASS"]);
            }
        }

        public static string LogFilePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["LOG_FILE_PATH"]);
            }
        }

        public static string LogFilePathOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["LOG_FILE_PATH_ON_FIRST_SERVER"]);
            }
        }

        public static string LogFilePathOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["LOG_FILE_PATH_ON_SECOND_SERVER"]);
            }
        }

        public static string ArchiveLogFilePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["ARCHIVE_LOG_FILE_PATH"]);
            }
        }

        public static string FileStorageOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["FILE_STORAGE_ON_FIRST_SERVER"]);
            }
        }

        public static string FileStorageOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["FILE_STORAGE_ON_SECOND_SERVER"]);
            }
        }

        public static string FileStorageVirtualAddress
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["FILE_STORAGE_VIRTUAL_ADDRESS"]);
            }
        }

        public static string FileStorageTempDirectory
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["FILE_STORAGE_TEMP_DIRECTORY"]);
            }
        }

        public static string StaticStorageOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["STATIC_STORAGE_ON_FIRST_SERVER"]);
            }
        }

        public static string StaticStorageOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["STATIC_STORAGE_ON_SECOND_SERVER"]);
            }
        }

        public static string TempPreviewPagePath
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["TEMP_PREVIEW_PAGE_PATH"]);
            }
        }

        public static string CacheDependencyFile
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CACHE_DEPENDENCY_FILE"]);
            }
        }

        public static string CacheDependencyFileOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CACHE_DEPENDENCY_FILE_ON_FIRST_SERVER"]);
            }
        }

        public static string CacheDependencyFileOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["CACHE_DEPENDENCY_FILE_ON_SECOND_SERVER"]);
            }
        }

        public static string UploaderUserNameOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["UPLOADER_USERNAME_ON_FIRST_SERVER"]);
            }
        }

        public static string UploaderUserPasswordOnFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["UPLOADER_USERPASSWORD_ON_FIRST_SERVER"]);
            }
        }

        public static string DomainNameOfFirstServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["DOMAIN_NAME_OF_FIRST_SERVER"]);
            }
        }

        public static string UploaderUserNameOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["UPLOADER_USERNAME_ON_SECOND_SERVER"]);
            }
        }

        public static string UploaderUserPasswordOnSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["UPLOADER_USERPASSWORD_ON_SECOND_SERVER"]);
            }
        }

        public static string DomainNameOfSecondServer
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["DOMAIN_NAME_OF_SECOND_SERVER"]);
            }
        }

        public static string ServiceErrorLogFile
        {
            get
            {
                return AppConvert.ToString(ConfigurationSettings.AppSettings["SERVICE_ERROR_LOG_FILE_PATH"]);
            }
        }

        public static string CurrentDirName
        {
            get {
                string dirName = string.Empty;

                try
                {
                    if (System.IO.Directory.Exists(ConfigValues.CurrentDirFilePath))
                    {
                        string[] files = System.IO.Directory.GetFiles(ConfigValues.CurrentDirFilePath);
                        if (files.Length > 0)
                        {
                            dirName = (new System.IO.FileInfo(files[files.Length - 1])).Name;
                        }
                    }
                }
                catch
                {

                }

                return dirName;
            }
        }
    }
}
