"use strict";

function FileImport() {

    var FIH = FileImportHandler();

    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var rackHash = urlParams.get("rackHash");

        //MH.getAllDatabases()
        //    .then(allDatabasesArrived);
    });
}

FileImport();
