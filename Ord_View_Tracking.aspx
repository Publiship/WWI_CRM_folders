<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="Ord_view_Tracking.aspx.cs" Inherits="Ord_view_Tracking" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="content_logbook" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">


<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript">

    var mygallery = new fadeSlideShow({
        wrapperid: "fadeshow1", //ID of blank DIV on page to house Slideshow
        dimensions: [760, 250], //width/height of gallery in pixels. Should reflect dimensions of largest image
        imagearray: [
		["Images/intro1b.jpg", "", "", "Specialist logistics for the printing and publishing industry."],
		["Images/intro2b.jpg", "", "", "On-line shipment tracking."],
		["Images/intro3b.jpg", "", "_new", "Sea, Air and Road freight"] //<--no trailing comma after very last image element!
	],
        displaymode: { type: 'auto', pause: 2500, cycles: 0, wraparound: false },
        persist: false, //remember last viewed slide and recall within same session?
        fadeduration: 2000, //transition duration (milliseconds)
        descreveal: "peekaboo",
        togglerid: ""
    })

</script>
            <div class="innertubefxhw">
                <div class="slideshows">
                    <div id="fadeshow1"></div>
                </div> <!-- end slide show container -->
                <h1>Welcome to Publiship</h1>
                <!-- <div class="whiteblock"> -->
                <div class="commentbox">
                "I thought shipping was fraught with pitfalls, strange terminology and unplanned for costs; 
                that is until I swapped our business to Publiship! 
                Now I know what I am paying, know where my books are and most importantly 
                know they will arrive on time"
                </div>
                <div class="commentfooter">
                 </div> 
                <p>
                So said the Production Director of 
                    a blue-chip international publishing house when asked about 
                our service.
                </p>
                <p>
                In the difficult trading conditions that exist in the world market today, it's more important 
                than ever to ensure that costs are under control, and your supply chain is efficient and reliable.
                </p>
                <p>Publiship have been providing logistics services to the publishing industry for over 25 years.</p>
                <p> 
                When every facet of the logistics process is under scrutiny, it's no surprise that more and more 
                publishers and printers are turning to Publiship. 
                Our experience, plus a range of useful software tools that allow for fixed cost, per copy pricing, 
                puts us at the forefront of logistics provision for bookshippers.
                </p>
                <p>
                Take a look around our website, and find out why Publiship would be right for you.
              </p>
       
             </div> <!-- end inner tube -->
     <div>
     </div>
    
</asp:Content>

