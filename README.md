# EasyClass
to run the code:
Install-Package EasyClass

 var ec = new EasyClass.WebReader();
 ec.BuildClassFromJson(
                "https://yoururlwebapi.com/2/queries/methodname?action=run&inputnumber=1234",   //WebApi address
                "NamespaceName",   //output name you want for namespace
		"ClassName",   //Class name you want the file to be called
		@"c:\temp");   //Folder where you want the class file to be generated, this parameter is optional.


if your url is secure and require a key use the following code:

 var ec = new EasyClass.WebReader();
ec.AddHeader = true;
            ec.HeaderKey = "securitykey";
            ec.HeaderValue = "1234567890key";
 ec.BuildClassFromJson(
                "https://yoururlwebapi.com/2/queries/methodname?action=run&inputnumber=1234",   //WebApi address
                "NamespaceName",   //output name you want for namespace
		"ClassName",   //Class name you want the file to be called
		@"c:\temp");   //Folder where you want the class file to be generated, this parameter is optional.

