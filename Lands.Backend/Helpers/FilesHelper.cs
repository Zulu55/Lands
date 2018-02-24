namespace Lands.Backend.Helpers
{
    using System.IO;
    using System.Web;

    public class FilesHelper
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
            }

            return pic;
        }

        public static bool UploadPhoto(MemoryStream stream, string folder, string name)
        {
            try
            {
                stream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}