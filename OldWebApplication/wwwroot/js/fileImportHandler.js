function FileImportHandler() {

    const uri = 'api/FileImport';

    function getRackInfo(rackHash) {
        return $.getJSON(uri + "/getRackInfo", { rackHash: rackHash });
    }

    function getDirectoryInfo(rackHash, subDirectory) {
        var dto = { rackHash: rackHash, subDirectory: subDirectory };
        return $.getJSON(uri + "/getDirectoryInfo", dto);
    }

    function parseDirectoryContent(rackHash, subDirectory) {
        var dto = { rackHash: rackHash, subDirectory: subDirectory };
        return $.getJSON(uri + "/parseDirectoryContent", dto);
    }

    function getThreadInfo(rackHash, subDirectory) {
        var dto = { rackHash: rackHash, subDirectory: subDirectory };
        return $.getJSON(uri + "/getThreadInfo", dto);
    }

    return {
        getRackInfo: getRackInfo,
        getDirectoryInfo: getDirectoryInfo,
        parseDirectoryContent: parseDirectoryContent,
        getThreadInfo: getThreadInfo,
    }
}