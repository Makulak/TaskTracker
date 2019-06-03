using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace TaskTracker.Models
{
    class XamarinImage
    {
        [JsonProperty("image")]
        private string Base64Image { get; set; }

        public XamarinImage(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using (MemoryStream m = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    m.Write(buffer, 0, read);
                }
                Base64Image = Convert.ToBase64String(m.ToArray());
            }
        }
    }
}
