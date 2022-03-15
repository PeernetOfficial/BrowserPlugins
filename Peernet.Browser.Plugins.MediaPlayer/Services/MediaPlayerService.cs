using Peernet.Browser.Plugins.MediaPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Peernet.SDK.Client.Http;
using Peernet.SDK.Common;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.MediaPlayer.Services
{
    public class MediaPlayerService : IPlayButtonPlug
    {
        private readonly ISettingsManager settingsManager;

        public MediaPlayerService(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var source = GetFileSource(file);
            var viewModel = new FileStreamViewModel();
            await viewModel.Prepare(new (source, file.Type, file.Name));
            new FileStreamWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Type is LowLevelFileType.Video or LowLevelFileType.Audio)
            {
                return true;
            }

            return false;
        }

        private Uri GetFileSource(ApiFile file)
        {
            // ISettingsManager.ApiKey
            var parameters = new Dictionary<string, string>
            {
                ["hash"] = Convert.ToHexString(file.Hash),
                ["node"] = Convert.ToHexString(file.NodeId),
                ["format"] = "14",
                ["k"] = settingsManager.ApiKey
            };

            var uriBase = $"{settingsManager.ApiUrl}/file/view";

            var requestMessage = HttpHelper.PrepareMessage(uriBase, HttpMethod.Get, parameters, null);
            return requestMessage.RequestUri;
        }
    }
}