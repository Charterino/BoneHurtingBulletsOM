using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Eventing;
using OpenMod.API.Users;
using OpenMod.Core.Eventing;
using OpenMod.Core.Users;
using OpenMod.Unturned.Players.Life.Events;
using OpenMod.Unturned.Users;

namespace BoneHurtingBullets
{
    public class DamageEventListener : IEventListener<UnturnedPlayerDamagedEvent>
    {
        private readonly BoneHurtingBulletsPlugin _pluginInstance;
        
        public DamageEventListener(BoneHurtingBulletsPlugin pluginInstance)
        {
            _pluginInstance = pluginInstance;
        }
        
        public async Task HandleEventAsync(object sender, UnturnedPlayerDamagedEvent @event)
        {
            await _pluginInstance.HandleEvent(@event.Player, @event.Limb);
        }
    }
}