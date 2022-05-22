"use strict";

function GridContainerBuilder() {

    function buildGridContainer() {
        var $gridDivs = $(''
            + '<div id="GridContainer" class="grid-container" >'
            + '  <div id="HeaderDiv" class="headerArea">'
            + '  </div>'
            + '  <div id="MenuDiv" class="menuArea">'
            + '  </div>'
            + '  <div id="MainDiv" class="mainArea">'
            + '  </div>'
            + '  <div id="RightDiv" class="rightArea">'
            + '  </div>'
            + '  <div id="FooterDiv" class="footerArea">'
            + '  </div>'
            + '</div>'
        );
        $(document.body).append($gridDivs);
    }

    function moveElementsToGrids() {
        moveStyleToDiv('toHeader', 'HeaderDiv');
        moveStyleToDiv('toMenu', 'MenuDiv');
        moveStyleToDiv('toMain', 'MainDiv');
        moveStyleToDiv('toRight', 'RightDiv');
        moveStyleToDiv('toFooter', 'FooterDiv');
    }

    function moveStyleToDiv(styleName, divId) {
        var $styleElements = $('.' + styleName);
        var $div = $('#' + divId);
        $div.append($styleElements);
    }

    $(function () {
        buildGridContainer();
        moveElementsToGrids();
    });
}

GridContainerBuilder();
