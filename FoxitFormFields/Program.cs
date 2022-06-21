using foxit.common;
using foxit.common.fxcrt;
using foxit.pdf;
using foxit.pdf.interform;

// initialize the Foxit PDF SDK library
string sn = "<your_sn>";
string key = "<your_key>";
ErrorCode error_code = Library.Initialize(sn, key);
if (error_code != ErrorCode.e_ErrSuccess)
{
    Library.Release();
    return;
}

// load our Sample.pdf file
PDFDoc doc = new PDFDoc("Sample.pdf");
error_code = doc.Load(null);
if (error_code != ErrorCode.e_ErrSuccess)
{
    Library.Release();
    return;
}

// let's get the first page into an object
PDFPage page = doc.GetPage(0);
// Parse page
page.StartParse((int)foxit.pdf.PDFPage.ParseFlags.e_ParsePageNormal, null, false);

// our Sample.pdf file does not have a Form object, so let's create one
Form form = new Form(doc);

// add a TextField called "FirstName"
Control control = form.AddControl(page, "FirstName",
    Field.Type.e_TypeTextField, new RectF(180f, 600f, 350f, 630f));
// see if there is a command line argument with the FirstName specified already
if (args.Length > 0)
{
    if (args[0].Length > 0)
    {
        Field field = form.GetField(0, "FirstName");
        field.SetValue(args[0].ToString());
    }
}

// add a TextField called "LastName"
form.AddControl(page, "LastName",
    Field.Type.e_TypeTextField, new RectF(180f, 560f, 350f, 590f));
// see if there is a command line argument with the LastName specified already
if (args.Length > 0)
{
    if (args[1].Length > 0)
    {
        Field field = form.GetField(0, "LastName");
        field.SetValue(args[1].ToString());
    }
}

// add a TextField called "PhoneNumber"
form.AddControl(page, "PhoneNumber",
    Field.Type.e_TypeTextField, new RectF(180f, 520f, 350f, 550f));

// add a TextField called "InsuranceProvider"
form.AddControl(page, "InsuranceProvider",
    Field.Type.e_TypeTextField, new RectF(180f, 410f, 350f, 440f));

// add a TextField called "MemberNumber"
form.AddControl(page, "MemberNumber",
    Field.Type.e_TypeTextField, new RectF(180f, 370f, 350f, 400f));

// add a TextField called "MemberSince"
form.AddControl(page, "MemberSince",
    Field.Type.e_TypeTextField, new RectF(180f, 330f, 350f, 360f));

// Save the modified PDF to a new file
string newPdf = "Sample_With_Fields.pdf";
doc.SaveAs(newPdf, (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);

// good practice to release the library when everything is done
Library.Release();