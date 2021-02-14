
    "use strict";
    function toastyInfo(msg) {
        $.toast({
            heading: "Informa��o!",
            text: msg,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: 'info',
            hideAfter: 3000,
            stack: 6
        });
    }

    function toastyWarning(msg) {
        $.toast({
            heading: "Aten��o!",
            text: msg,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: 'warning',
            hideAfter: 3500,
            stack: 6
        });
    }

    function toastySuccess(msg) {
        $.toast({
            heading: "Sucesso",
            text: msg,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: 'success',
            hideAfter: 3500,
            stack: 6
        });
    }

    function toastyError(msg) {
        $.toast({
            heading: "Error!",
            text: msg,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: 'error',
            hideAfter: 3500
        });
    }
