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

        var $dirTableBody = $('#DirectoriesListBody');
        $dirTableBody.empty();
        for (let i = 0; i < rackInfo.subDirectories.length; i++) {
            addDirectory($dirTableBody, rackInfo.subDirectories[i]);
        }
    }

    function addDirectory($dirTableBody, subDirectory) {
        var $tr = $dirTableBody.append('<tr>');
        var $td = $('<td> </td>').html(subDirectory);
        $tr.append($td);
    }

    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var rackHash = urlParams.get("rackHash");

        FIH.getRackInfo(rackHash)
            .then(rackInfoArrived);
    });
}

FileImport();
