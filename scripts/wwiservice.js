var pricerProxy;

// Initializes global and proxy default variables.
function pageLoad() {
    // Instantiate the service proxy.
    pricerProxy = new DAL.Services.Publiship_Pricer();

    // Set the default call back functions.
    pricerProxy.set_defaultSucceededCallback(SucceededCallback);
    pricerProxy.set_defaultFailedCallback(FailedCallback);
}


// Processes the button click and calls
// the service getPriceQuote method.
function OnClickQuote(Dimensions, Currency, Title, Length, Width, Depth, Weight, Carton, Origin, Final_Destination, Copies) {
    var quote = pricerProxy.GetPriceQuote(Dimensions, Currency, Title, Length, Width, Depth, Weight, Carton, Origin, Final_Destination, Copies);
}

// Callback function that
// processes the service return value.
function SucceededCallback(result) {
    //var RsltElem = document.getElementById("dxlblquote");
    //RsltElem.innerHTML = result;
    return result;
}

// Callback function invoked when a call to 
// the  service methods fails.
function FailedCallback(error, userContext, methodName) {
    if (error !== null) {
        //var RsltElem = document.getElementById("dxlblquote");
        //RsltElem.innerHTML = "An error occurred: " +
        //  error.get_message();
        return error.get_message();
    }
}

if (typeof (Sys) !== "undefined") Sys.Application.notifyScriptLoaded();