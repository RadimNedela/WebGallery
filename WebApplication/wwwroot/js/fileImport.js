"use strict";

function FileImport() {

    var FIH = FileImportHandler();
    var rackInfo;

    function rackInfoArrived(data) {
        rackInfo = data;
        $('#ActiveDatabaseNameDiv').html(rackInfo.activeDatabaseName);
        $('#ActiveDatabaseHashDiv').html(rackInfo.activeDatabaseHash);
        $('#ActiveRackNameDiv').html(rackInfo.activeRackName);
        $('#ActiveRackHashDiv').html(rackInfo.activeRackHash);
    }

    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var rackHash = urlParams.get("rackHash");

        FIH.getRackInfo(rackHash)
            .then(rackInfoArrived);
    });
}

FileImport();
