using Hkmp.Math;
using HkmpPouch;
using static KonpanionDx.Utilities;
namespace KonpanionDx
{
    internal class KonpanionDxUpdate : PipeEvent
    {
        public static string Name = "KonpanionDxUpdate";
        public Vector2 pos { get; set; }
        public State anim { get; set; }
        public Direction dir { get; set; }
        public override string GetName() => KonpanionDxUpdate.Name;

        public override string ToString()
        {
            return $"{i2s((int)anim)}{Constants.Separator}{i2s((int)dir)}{Constants.Separator}{f2s(pos.X)}{Constants.Separator}{f2s(pos.Y)}";
        }
    }

    internal class KonpanionDxUpdateFactory : IEventFactory
    {
        public static KonpanionDxUpdateFactory Instance { get; internal set; } = new KonpanionDxUpdateFactory();

        public PipeEvent FromSerializedString(string serializedData)
        {
            var pEvent = new KonpanionDxUpdate();
            var Split = serializedData.Split(Constants.SplitSep);
            pEvent.anim = (State)s2i(Split[0]);
            pEvent.dir = (Direction)s2i(Split[1]);
            pEvent.pos = new Vector2(s2f(Split[2]), s2f(Split[3]));
            return pEvent;
        }

        public string GetName() => KonpanionDxUpdate.Name;
    }
}
