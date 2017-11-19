$(document).ready(function () {
    //Get all elements that have the 'clamp' attribute
    const clampElements = $("[clamp]");

    clampElements.each(function (index, element) {
        //Truncate the line defined in the clamp attribute
        let clampValue = $(element).attr("clamp");
        $clamp(element, { clamp: clampValue });
    });
});