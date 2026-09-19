using Intersect.Server.General;
using Intersect.Server.Core.CommandParsing;
using Intersect.Server.Core.CommandParsing.Arguments;
using Intersect.Server.Localization;
using Intersect.Server.Networking;

namespace Intersect.Server.Core.Commands
{

    internal sealed partial class EquipTitleCommand : TargetClientCommand
    {

        public EquipTitleCommand() : base(
            Strings.Commands.EquipTitle, Strings.Commands.Arguments.TargetTitle,
            new VariableArgument<string>(Strings.Commands.Arguments.TitleName, RequiredIfNotHelp, true)
        )
        {
        }

        private VariableArgument<string> TitleNameArgument => FindArgumentOrThrow<VariableArgument<string>>();

        protected override void HandleTarget(ServerContext context, ParserResult result, Client target)
        {
            if (target?.Entity == null)
            {
                Console.WriteLine($@"    {Strings.Player.Offline}");

                return;
            }

            var player = target.Entity;
            var titleName = result.Find(TitleNameArgument);

            if (string.Equals(titleName, "none", StringComparison.OrdinalIgnoreCase))
            {
                player.RemoveTitle();
                player.User?.Save();
                Console.WriteLine($@"    {Strings.Commandoutput.TitleRemoved.ToString(player.Name)}");

                return;
            }

            var title = TitleManager.FindByName(titleName);
            if (title == null)
            {
                Console.WriteLine($@"    {Strings.Commandoutput.TitleNotFound.ToString(titleName)}");

                return;
            }

            if (!player.EquipTitle(title.Id))
            {
                Console.WriteLine($@"    {Strings.Commandoutput.TitleNotUnlocked.ToString(player.Name, title.Name)}");

                return;
            }

            player.User?.Save();
            Console.WriteLine($@"    {Strings.Commandoutput.TitleEquipped.ToString(player.Name, title.Name)}");
        }

    }

}
