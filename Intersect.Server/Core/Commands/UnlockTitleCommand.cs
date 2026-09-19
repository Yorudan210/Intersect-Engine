using Intersect.Server.General;
using Intersect.Server.Core.CommandParsing;
using Intersect.Server.Core.CommandParsing.Arguments;
using Intersect.Server.Localization;
using Intersect.Server.Networking;

namespace Intersect.Server.Core.Commands
{

    internal sealed partial class UnlockTitleCommand : TargetClientCommand
    {

        public UnlockTitleCommand() : base(
            Strings.Commands.UnlockTitle, Strings.Commands.Arguments.TargetTitle,
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

            var titleName = result.Find(TitleNameArgument);
            var title = TitleManager.FindByName(titleName);
            if (title == null)
            {
                Console.WriteLine($@"    {Strings.Commandoutput.TitleNotFound.ToString(titleName)}");

                return;
            }

            var player = target.Entity;
            if (!player.UnlockTitle(title.Id))
            {
                Console.WriteLine($@"    {Strings.Commandoutput.TitleAlreadyUnlocked.ToString(player.Name, title.Name)}");

                return;
            }

            player.User?.Save();
            Console.WriteLine($@"    {Strings.Commandoutput.TitleUnlocked.ToString(player.Name, title.Name)}");
        }

    }

}
