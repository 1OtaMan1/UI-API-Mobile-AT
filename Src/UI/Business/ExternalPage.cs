using Atata;

namespace UI.Business;

public class ExternalPage : Page<ExternalPage>
{
    [FindByCss("body")]
    public Control<ExternalPage> Body { get; private set; }
}