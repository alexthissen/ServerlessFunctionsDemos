using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using QRCoder;

namespace ServerlessFunctionsApp
{
    public static class QRCodeGeneratorFunction
    {
        [FunctionName("QRCodeGeneratorFunction")]
        public static async Task Run(
            [QueueTrigger("azurefunctions-qrcodes-requests",
                Connection = "azurefunctions-queues")]string imageText,
            [Blob("azurefunctions-qrcode-images/{rand-guid}",
                FileAccess.ReadWrite,
                Connection = "azurefunctions-blobs")] CloudBlockBlob blob,
            TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {imageText}");

            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(imageText, QRCodeGenerator.ECCLevel.H);
            QRCode code = new QRCode(data);

            using (var stream = await blob.OpenWriteAsync())
            {
                Bitmap bitmap = code.GetGraphic(20, Color.Black, Color.White, true);
                bitmap.Save(stream, ImageFormat.Png);
            }
        }

    }
}