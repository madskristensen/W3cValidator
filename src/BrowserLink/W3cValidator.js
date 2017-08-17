(function (browserLink, $) {

    var _project;

    function initialize(project) {
        _project = project;
    }

    function validate() {
        callApi(function (jsonText) {
            browserLink.invoke("Report", jsonText, window.location.href, _project);
        });
    }

    function callApi(callback) {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "https://html5.validator.nu?out=json&level=error&laxtype=yes", true);
        xhr.setRequestHeader("Content-type", "text/html");

        xhr.onload = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                callback(xhr.responseText);
            }
        };

        var node = document.doctype;
        var doctype = "<!DOCTYPE "
            + node.name
            + (node.publicId ? ' PUBLIC "' + node.publicId + '"' : '')
            + (!node.publicId && node.systemId ? ' SYSTEM' : '')
            + (node.systemId ? ' "' + node.systemId + '"' : '')
            + '>';

        xhr.send(doctype + document.documentElement.outerHTML);
    }

    return {
        initialize: initialize,
        validate: validate,

        menu: {
            displayText: 'W3C Validator',
            'Validate': 'validate'
        }
    };
});