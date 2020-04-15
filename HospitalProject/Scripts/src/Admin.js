window.onload = function () {



}

function loginCheck() {
    var password = "password";

    if (window.location == "https://localhost:44308/FrequentlyAskedQuestion/ListFAQ") {
        var enteredPassword = prompt('Please admin password:');

        if (password == enteredPassword) {
            location.replace("https://localhost:44308/FrequentlyAskedQuestion/ListFAQAdmin");
            return false;
        }

        else
            return false;
    }
    else {

        var enteredPassword = prompt('Please admin password:');

        if (password != enteredPassword) {
            return false;
        }
    }
}