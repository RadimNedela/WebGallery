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

        directoryInfoArrived(rackInfo.directoryInfo);
    }

    function directoryInfoArrived(directoryInfo) {
        fillDirectoriesTable(directoryInfo);
        fillFilesTable(directoryInfo);
    }

    function subDirectoryClicked(e) {
        var subDirectory = e.data.subDirectory;
        FIH.getDirectoryInfo(rackInfo.activeRackHash, subDirectory)
            .then(directoryInfoArrived);
    }

    function fillDirectoriesTable(directoryInfo) {
        var $dirTableBody = $('#DirectoriesListBody');
        $dirTableBody.empty();
        for (let i = 0; i < directoryInfo.subDirectories.length; i++) {
            addDirectory($dirTableBody, directoryInfo.subDirectories[i]);
        }
    }

    function addDirectory($dirTableBody, subDirectory) {
        var $tr = $dirTableBody.append('<tr>');
        var $td = $('<td> </td>').html(subDirectory);
        $td.click({ subDirectory: subDirectory }, subDirectoryClicked);
        $tr.append($td);
    }

    function fillFilesTable(directoryInfo) {
        var $filesTableBody = $('#FilesListBody');
        $filesTableBody.empty();
        for (let i = 0; i < directoryInfo.files.length; i++) {
            addFile($filesTableBody, directoryInfo.files[i]);
        }
    }

    function addFile($filesTableBody, file) {
        var $tr = $filesTableBody.append('<tr>');
        var $td = $('<td> </td>').html(file);
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
