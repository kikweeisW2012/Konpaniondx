using HkmpPouch;
using System;
using UnityEngine;

namespace KonpanionDx
{
    internal static class SafePouchIntegration
    {
        internal static PipeClient pipe = new PipeClient("KonpanionDx");

    }
    internal static class PouchIntegration
    {
        internal static string oldUpdate = null;
        internal static void Initialize()
        {
            SafePouchIntegration.pipe.On(KonpanionDxUpdateFactory.Instance).Do<KonpanionDxUpdate>(KonpanionDxUpdateHandler);
        }
        internal static void SendUpdate(CompanionControl companionControl)
        {
            if (SafePouchIntegration.pipe != null && SafePouchIntegration.pipe.ClientApi != null && SafePouchIntegration.pipe.ClientApi.NetClient.IsConnected)
            {

                var newUpdate = new KonpanionDxUpdate
                {
                    pos = (Hkmp.Math.Vector2)(Vector2)companionControl.transform.position,
                    dir = companionControl.lookDirection,
                    anim = companionControl.state
                };

                if (oldUpdate != newUpdate.ToString()) {

                    SafePouchIntegration.pipe.Broadcast(newUpdate);
                    oldUpdate = newUpdate.ToString();
                }
            }
        }

        internal static void KonpanionDxUpdateHandler(KonpanionDxUpdate update)
        {
            var _go = KonpanionDx.Instance.GetNetworkKonpanionDx(update.FromPlayer);
            var _control = _go.GetComponent<CompanionControl>();
            _control.state = update.anim;
            _control.networkMovementTarget = new Vector2(update.pos.X, update.pos.Y);
            _control.lookDirection = update.dir;
            _control.isNetworkControlled = true;
            _control.UpdateNetworkCoro();
        }
    }
}
