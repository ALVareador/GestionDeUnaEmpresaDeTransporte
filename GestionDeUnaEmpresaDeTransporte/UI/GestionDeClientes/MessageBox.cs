using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;


namespace GestionClientesUI;

public class MessageBox: Window
{
    public MessageBox()
    {
        this.Build();
    }
    
    void Build()
    {
        var pnlMain = new DockPanel {
            Margin = new Thickness( 5 )
        };
        this.lblMessage = new Label {
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness( 5 )
        };
        var btOk = new Button {
            Content = "Ok",
            Margin = new Thickness(5 ),
            HorizontalAlignment = HorizontalAlignment.Right
        };

        btOk.Click += (_, _) => this.Close();

        pnlMain.Children.AddRange( new Control[]{ lblMessage, btOk } );
        this.Content = pnlMain;
    }

    public string Message {
        get {
            return (string) this.lblMessage.Content;
        }
        set {
            this.lblMessage.Content = value;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }

    Label lblMessage;
}