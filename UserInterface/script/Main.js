"use strict";

function main() {
    function getDatabase() {
        $('#podTlacitkem').text("asdf");
        //return { Database: "asdf" };
    }

    return {
        GetDatabase: getDatabase,
    };
}

if (!document.MainScript)
    document.MainScript = main();
