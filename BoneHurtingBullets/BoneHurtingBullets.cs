using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Players;
using OpenMod.Unturned.Users;
using SDG.Unturned;

[assembly: PluginMetadata("BoneHurtingBullets", DisplayName = "BoneHurtingBullets")]

namespace BoneHurtingBullets
{
    public class BoneHurtingBulletsPlugin : OpenModUnturnedPlugin
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BoneHurtingBulletsPlugin> _logger;
        private readonly Random _random;
        public BoneHurtingBulletsPlugin(
            IConfiguration configuration,
            ILogger<BoneHurtingBulletsPlugin> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _random = new Random();
        }

        protected override async UniTask OnLoadAsync()
        {
            _logger.LogInformation("BoneHurtingBullets for OM by Charterino!");
            _logger.LogInformation("Original idea belongs to IcePlugins.");
        }

        protected override async UniTask OnUnloadAsync()
        {
            
        }

        public async Task HandleEvent(UnturnedPlayer target, ELimb limb)
        {
            await UniTask.SwitchToMainThread();
            var chances = _configuration.GetSection("chances").Get<LimbChance[]>();
            var chance = chances.FirstOrDefault(x => x.Limb == limb.ToString());
            if (chance != null)
                if (_random.Next(0, 100) <= chance.Chance)
                    target.Player.life.breakLegs();
            await UniTask.SwitchToThreadPool();
        }
    }
}