using System;

using System.Drawing;

using System.Collections;

using System.ComponentModel;

using System.Windows.Forms;

using System.IO;

using System.Threading;

using System.Net.Sockets;

 

namespace Servidor

{

   public class Server : System.Windows.Forms.Form

   {

      private System.Windows.Forms.TextBox Envia;

      private System.Windows.Forms.TextBox Exibe;

      private Socket conexao;

      private Thread tipoThread;

 

      private System.ComponentModel.Container components = null;

      private NetworkStream socketStream;

      private BinaryWriter escreve;

      private BinaryReader le;

 

      public Server()

      {

         InitializeComponent();

         // thread para aceitar multiplas conexões

         tipoThread = new Thread( new ThreadStart( RunServer ) );

         tipoThread.Start();

      }

 

      /// <summary>

      /// Clean up any resources being used.

      /// </summary>

      protected override void Dispose( bool disposing )

      {

         if( disposing )

         {

            if(components != null)

            {

               components.Dispose();

            }

         }

         base.Dispose( disposing );

      }

 

      #region Windows Form Designer generated code

      /// <summary>

      /// Required method for Designer support - do not modify

      /// the contents of this method with the code editor.

      /// </summary>

      private void InitializeComponent()

      {

          this.Exibe = new System.Windows.Forms.TextBox();

          this.Envia = new System.Windows.Forms.TextBox();

          this.SuspendLayout();

          this.Exibe.Location = new System.Drawing.Point(8, 40);

          this.Exibe.Multiline = true;

          this.Exibe.Name = "Exibe";

          this.Exibe.Size = new System.Drawing.Size(299, 238);

          this.Exibe.TabIndex = 1;

          this.Envia.Location = new System.Drawing.Point(8, 8);

          this.Envia.Name = "Envia";

          this.Envia.Size = new System.Drawing.Size(272, 20);

          this.Envia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Envia_KeyDown);

          this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);

          this.ClientSize = new System.Drawing.Size(299, 278);

          this.Controls.Add(this.Exibe);

          this.Controls.Add(this.Envia);

          this.Name = "Server";

          this.Text = "Servidor";

          this.Closing += new System.ComponentModel.CancelEventHandler(this.Server_Closing);

          this.Load += new System.EventHandler(this.Server_Load);

          this.ResumeLayout(false);

          this.PerformLayout();

 

      }

      #endregion

 

      [STAThread]

      static void Main()

      {

         Application.Run( new Server() );

      }

 

      protected void Server_Closing(

         object sender, CancelEventArgs e )

      {        

         System.Environment.Exit( System.Environment.ExitCode );

      }

 

      protected void Envia_KeyDown( 

         object sender, KeyEventArgs e )

      {

         // Aqui está o código responsável para mandar mensagens

         try

         {        

            if ( e.KeyCode == Keys.Enter && conexao != null )

            {

               writer.Write( Envia.Text );

        

               Exibe.Text += Envia.Text;

               if ( Envia.Text == "FINALIZAR" )

                  conexao.Close();

 

               Envia.Clear();

            }

         }

         catch ( SocketException )

         {

            Exibe.Text += "Atneção! Erro...";

         }

      }

 

      public void RunServer()

      {

         TcpListener escutando;

         int conta = 1; //contaremos quantas conexões teremos

         try

         {

            escutando = new TcpListener( 9000 );

            escutando.Start();

            while ( true )

            {

               Exibe.Text = "Aguardando Conexoes";

               conexao = escutando.AcceptSocket();

               escreve = new BinaryWriter( socketStream );

               le = new BinaryReader( socketStream );

               socketStream = new NetworkStream( conexao );

 

               Exibe.Text += conta + " Conexões Recebidas!";

                  escreve.Write( "Cenexão Efetuada!" );

 

               Envia.ReadOnly = false;

               string resp = "";

               do

               {

                  try

                  {  

                     resp = le.ReadString();

                     Exibe.Text += "\r\n" + resp;

                  }

                  catch ( Exception )

                  {

                     break;

                  }

 

               } while ( resp != "FIM"  && conexao.Connected );

 

               Exibe.Text += "Conexão Finalizada!";

               //fechando a conexao

               escreve.Close();

               le.Close();

               socketStream.Close();

               conexao.Close();

 

               ++conta;

            }

         }

 

         catch ( Exception error )

         {

            MessageBox.Show( error.ToString() );

         }

 

      }

 

   }

}