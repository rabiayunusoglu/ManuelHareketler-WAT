
(function (angular) {
    'use strict';
    var manuelApp = angular.module('app', [
        'ngRoute',
        'ui.bootstrap'
    ]).run(function ($rootScope) {

        // http requestlerinde url başlangıcı
        $rootScope.url = '';

    });
    //User
    manuelApp.controller('homeUserCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.manualActionCount = 0;
        $scope.noStartedCount = 0;
        $scope.startedCount = 0;
        $scope.finishedCount = 0;
        $scope.uploading = true;
        $scope.fetchData = function () {
            $scope.uploading = false;
            $http.get($rootScope.url + "/ManualAction/GetCountByRegNo").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.manualActionCount = response.data[0];
                    $scope.finishedCount = response.data[3];
                    $scope.noStartedCount = response.data[1];
                    $scope.startedCount = response.data[2];
                }

                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

    }])
    manuelApp.controller('manualActionUserCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.manualActionList = [];
        $scope.noStartedList = [];
        $scope.startedList = [];
        $scope.finishedList = [];
        $scope.redirectItem = {};
        $scope.finishItem = {};
        $scope.uploading = true;
        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;
        $scope.currentPage2 = 1;
        $scope.itemsPerPage2 = 10;
        $scope.currentPage3 = 1;
        $scope.itemsPerPage3 = 10;
        $scope.currentPage4 = 1;
        $scope.itemsPerPage4 = 10;
        $scope.departmantList = [];
        $scope.userList = [];
        $scope.reasonList = [];
        $scope.startDate = new Date().toISOString().substr(0, 10);
        $scope.endDate = new Date().toISOString().substr(0, 10);

        $scope.fetchData = function () {
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http.get($rootScope.url + "/ManualAction/GetManagerByRegNo").then(function (response) {
                if (response != null || response.data != null) {
                    var i = 0; var j = 0; var k = 0; var l = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };
        $scope.fetchDataByDate = function () {
            $scope.setDateFilter();
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http({
                method: "get",
                url: $rootScope.url + `/ManualAction/GetByDateByRegNo?startDate=${$scope.startDate}&endDate=${$scope.endDate}`,
            }).then(function (response) {
                if (response != null || response.data != null) {
                    var i = 0; var j = 0; var k = 0; var l = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };
        $scope.fetchDepartmant = function () {
            $scope.departmantList = [];
            $http.get($rootScope.url + "/Departmant/GetManager").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.departmantList = response.data;
                    $scope.departmant = response.data.map((x) => x.departmentName);
                }
            }, function () {
                $scope.departmantList = [];
            });
        };
        $scope.fetchUser = function () {
            $scope.userList = [];
            var id = $scope.departmantList.filter(t => t.departmentName == $scope.redirectItem.redirectedTeam).map(x => x.departmentType);
            $http.get($rootScope.url + "/User/GetManager").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.userList = response.data.filter((x) => x.departmantType == id && x.userType == 'U');
                }
            }, function () {
                $scope.userList = [];
            });
        };
        $scope.fetchReason = function () {
            $scope.reasonList = [];
            $http.get($rootScope.url + "/ManualActionReason/GetManager").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.reasonList = response.data.map((x) => x.reasonName);
                }
            }, function () {
            });
        };

        $scope.sortBy = function (column) {
            $scope.sortColumn = column;
            $scope.reverse = !$scope.reverse;
        };
        $scope.sortBy2 = function (column) {
            $scope.sortColumn2 = column;
            $scope.reverse2 = !$scope.reverse2;
        };
        $scope.sortBy3 = function (column) {
            $scope.sortColumn3 = column;
            $scope.reverse3 = !$scope.reverse3;
        };
        $scope.sortBy4 = function (column) {
            $scope.sortColumn4 = column;
            $scope.reverse4 = !$scope.reverse4;
        };

        $scope.toogleFinished = function (item) {
            $scope.finishItem = item;
            $scope.fetchReason();
        };
        $scope.toogleRedirected = function (item) {
            $scope.redirectItem = item;
            $scope.fetchDepartmant();
            $scope.fetchUser();
        };

        $scope.Finished = function (item) {
            if ($scope.finishItem.actionReason == null || $scope.finishItem.actionReason.length == 0) {
                toastr.warning("Sebep seçmelisiniz");
                return;
            }
            if ($scope.finishItem.reasonDetail == null || $scope.finishItem.reasonDetail.length == 0) {
                toastr.warning("Açıklama girmelisiniz");
                return;
            }
            $scope.uploading = false;
            item.statusType = 3;
            item.statusName = "Tamamlandı";
            item.madeDate = new Date();
            $http.post($rootScope.url + "/ManualAction/Edit", item).then(function (response) {
                if (response != null || response.data != false) {
                    toastr.success("Tamamlandı");
                    $scope.finishItem = [];
                }
                $scope.uploading = true;
            }, function () {
                toastr.error("Bir sorun oluştu");
                $scope.uploading = true;
                $scope.finishItem = [];
            });
        };
        $scope.Redirected = function (item) {
            if ($scope.redirectItem.redirectedTeam == null || $scope.redirectItem.redirectedTeam.length == 0) {
                toastr.warning("Departman seçmelisiniz");
                return;
            }
            if ($scope.redirectItem.redirectedUser == null || $scope.redirectItem.redirectedUser.length == 0) {
                toastr.warning("Sorumlu Birey seçmelisiniz");
                return;
            }
            if ($scope.redirectItem.redirectedText == null || $scope.redirectItem.redirectedText.length == 0) {
                $scope.redirectItem.redirectedText = " ";
                return;
            }
            $scope.uploading = false;
            console.log(item)
            item.userRegisterNo = item.redirectedUser.registerNo;
            item.username = item.redirectedUser.username;
            item.redirectedUser = item.redirectedUser.username;
            $http.post($rootScope.url + "/ManualAction/RedirectItem", item).then(function (response) {
                if (response != null || response.data != false) {
                    toastr.success("Yönlendirildi");
                    $scope.redirectItem = [];
                }
                $scope.uploading = true;
            }, function () {
                toastr.error("Bir sorun oluştu");
                $scope.uploading = true;
                $scope.redirectItem = [];
            });
        };

        $scope.setDateFilter = function () {
            console.log(document.getElementById("data-endDate").value);
            console.log(document.getElementById("data-startDate").value);
            if (document.getElementById("data-endDate").value == null || document.getElementById("data-startDate").value == null)
                return;
            $scope.endDate = document.getElementById("data-endDate").value;
            $scope.startDate = document.getElementById("data-startDate").value;
        };
    }])

    //Admin
    manuelApp.controller('homeCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.manualActionCount = 0;
        $scope.noStartedCount = 0;
        $scope.startedCount = 0;
        $scope.finishedCount = 0;
        $scope.uploading = true;
        $scope.fetchData = function () {
            $scope.uploading = false;
            $http.get($rootScope.url + "/ManualAction/GetCount").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.manualActionCount = response.data[0];
                    $scope.finishedCount = response.data[3];
                    $scope.noStartedCount = response.data[1];
                    $scope.startedCount = response.data[2];
                }

                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

    }])
    manuelApp.controller('manualActionCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.manualActionList = [];
        $scope.historyList = [];
        $scope.selectedItem = [];
        $scope.noStartedList = [];
        $scope.startedList = [];
        $scope.finishedList = [];
        $scope.uploading = true;
        $scope.uploading1 = true;
        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;
        $scope.currentPage2 = 1;
        $scope.itemsPerPage2 = 10;
        $scope.currentPage3 = 1;
        $scope.itemsPerPage3 = 10;
        $scope.currentPage4 = 1;
        $scope.itemsPerPage4 = 10;
        $scope.startDate = new Date().toISOString().substr(0, 10);
        $scope.endDate = new Date().toISOString().substr(0, 10);

        $scope.fetchData = function () {
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http.get($rootScope.url + "/ManualAction/GetManager").then(function (response) {
                if (response != null || response.data != null) {
                    var i = 0; var j = 0; var k = 0; var l = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };
        $scope.fetchHistory = function (id, item) {
            $scope.uploading1 = false;
            $scope.historyList = [];
            $scope.selectedItem = item;
            $http.get($rootScope.url + "/ActionHistory/GetManagerByManualID?manualID=" + id).then(function (response) {
                if (response != null || response.data != null) {
                    $scope.historyList = response.data;
                }
                $scope.uploading1 = true;
            }, function () {
                $scope.uploading1 = true;
            });
        };
        $scope.fetchDataByDate = function () {
            $scope.setDateFilter();
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http({
                method: "get",
                url: $rootScope.url + `/ManualAction/GetByDate?startDate=${$scope.startDate}&endDate=${$scope.endDate}`,
            }).then(function (response) {
                if (response != null || response.data != null) {
                    var i = 0; var j = 0; var k = 0; var l = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

        $scope.sortBy = function (column) {
            $scope.sortColumn = column;
            $scope.reverse = !$scope.reverse;
        };
        $scope.sortBy2 = function (column) {
            $scope.sortColumn2 = column;
            $scope.reverse2 = !$scope.reverse2;
        };
        $scope.sortBy3 = function (column) {
            $scope.sortColumn3 = column;
            $scope.reverse3 = !$scope.reverse3;
        };
        $scope.sortBy4 = function (column) {
            $scope.sortColumn4 = column;
            $scope.reverse4 = !$scope.reverse4;
        };

        $scope.setDateFilter = function () {
            console.log(document.getElementById("data-endDate").value);
            console.log(document.getElementById("data-startDate").value);
            if (document.getElementById("data-endDate").value == null || document.getElementById("data-startDate").value == null)
                return;
            $scope.endDate = document.getElementById("data-endDate").value;
            $scope.startDate = document.getElementById("data-startDate").value;
        };

        $scope.Export = function () {
            $scope.setDateFilter();
            if ($scope.manualActionList != null && $scope.manualActionList.length !== 0) {
                $scope.uploading = false;
                $http({
                    method: "get",
                    url: $rootScope.url + '/ManualAction/Export',
                    responseType: "blob"
                }).then(function (response) {
                    $scope.uploading = true;
                    var date = new Date().toISOString().substr(0, 10);
                    //var myBlob = new Blob([result.data], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'})
                    const url = window.URL.createObjectURL(response.data);
                    const link = document.createElement("a");
                    link.href = url;
                    link.setAttribute("download", `WAT-Manuel Hareketler//${date}.xlsx`); //or any other extension
                    document.body.appendChild(link);
                    link.click();
                    toastr.success("Excel Dosyasına Aktarıldı.");
                });
            } else {
                toastr.warning("Tablo Boş");
            }
        };
        $scope.ExportByDate = function () {
            $scope.setDateFilter();
            if ($scope.manualActionList != null && $scope.manualActionList.length !== 0) {
                $scope.uploading = false;
                $http({
                    method: "get",
                    url: $rootScope.url + `/ManualAction/ExportByDate?startDate=${$scope.startDate}&endDate=${$scope.endDate}`,
                    responseType: "blob"
                }).then(function (response) {
                    $scope.uploading = true;
                    var date = new Date().toISOString().substr(0, 10);
                    //var myBlob = new Blob([result.data], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'})
                    const url = window.URL.createObjectURL(response.data);
                    const link = document.createElement("a");
                    link.href = url;
                    link.setAttribute("download", `WAT-Manuel Hareketler//${date}.xlsx`); //or any other extension
                    document.body.appendChild(link);
                    link.click();
                    toastr.success("Excel Dosyasına Aktarıldı.");
                });
            } else {
                toastr.warning("Tablo Boş");
            }
        };
    }])
    manuelApp.controller('reportCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.lineList = [];
        $scope.lineLabel = [''];
        $scope.lineData = [''];

        $scope.colunmLabel = [''];
        $scope.colunmData = [''];
        $scope.columnList = [];

        $scope.total = [];
        $scope.uploading = true;

        var ict_unit = [];
        var efficiency = [];
        var colorByReason = [];
        var colorByMonth = [];

        $scope.fetchData = function () {
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http.get($rootScope.url + "/ManualAction/GetReport").then(function (response) {
                if (response != null || response.data != null) {
                    ict_unit = [];
                    efficiency = [];
                    colorByReason = [];
                    colorByMonth = [];
                    $scope.total = response.data[2];

                    $scope.columnList = response.data[0].map(x => {
                        return {
                            ...x, month: (x.sortingDate.substr(4, 2)), year: (x.sortingDate.substr(0, 4)),
                            colName: ((x.sortingDate.substr(4, 2)) == "01") ? "Ocak" : ((x.sortingDate.substr(4, 2)) == "02") ? "Şubat" : ((x.sortingDate.substr(4, 2)) == "03") ? "Mart" : ((x.sortingDate.substr(4, 2)) == "04") ? "Nisan" : ((x.sortingDate.substr(4, 2)) == "05") ? "Mayıs" : ((x.sortingDate.substr(4, 2)) == "06") ? "Haziran" : ((x.sortingDate.substr(4, 2)) == "07") ? "Temmuz" : ((x.sortingDate.substr(4, 2)) == "08") ? "Ağustos" : ((x.sortingDate.substr(4, 2)) == "09") ? "Eylül" : ((x.sortingDate.substr(4, 2)) == "10") ? "Ekim" : ((x.sortingDate.substr(4, 2)) == "11") ? "Kasım" : ((x.sortingDate.substr(4, 2)) == "12") ? "Aralık" : "",
                        }
                    });
                    $scope.colunmLabel = $scope.columnList.map(t => t.colName + "-" + t.year);
                    $scope.colunmData = $scope.columnList.map(t => t.sumAmount);

                    $scope.lineList = response.data[1];
                    $scope.lineLabel = $scope.lineList.map(t => t.actionReason == "-" ? "Sebep Girilmemiş" : t.actionReason);
                    $scope.lineData = $scope.lineList.map(t => t.sumAmount);

                    $scope.setChart($scope.lineLabel, $scope.lineData, $scope.lineList, $scope.colunmLabel, $scope.colunmData );
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

        $scope.setChartByDate = function () {

        }
        $scope.setChart = function (labelOfLine, dataOfLine, listOfLine, labelOfColunm, dataOfColunm) {
            setColorReason();
            setColorMonth();

            const reasonPie = document.getElementById('reasonPieChart');
            const reasonPieChart = new Chart(reasonPie, {
                type: 'pie',
                data: {
                    labels: labelOfLine,
                    datasets: [{
                        label: '# of Votes',
                        data: dataOfLine,
                        backgroundColor: colorByReason,
                        borderWidth: 1,
                        innerWidth: 2,
                        devicePixelRatio: 1,
                        categoryPercentage: 0.2,

                    }]
                },
            });
            const reasonBar = document.getElementById('reasonBarChart');
            const reasonBarChart = new Chart(reasonBar, {
                type: 'bar',
                data: {
                    labels: labelOfLine,
                    datasets: [{
                        label: '# Maliyeti',
                        data: dataOfLine,
                        backgroundColor: colorByReason,
                    }]
                },

            });

            const monthPie = document.getElementById('monthPieChart');
            const monthPieChart = new Chart(monthPie, {
                type: 'pie',
                data: {
                    labels: labelOfColunm,
                    datasets: [{
                        label: '# Ay',
                        data: dataOfColunm,
                        backgroundColor: colorByMonth,
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
            const monthBar = document.getElementById('monthBarChart');
            const monthBarChart = new Chart(monthBar, {
                type: 'bar',
                data: {
                    labels: labelOfColunm,
                    datasets: [{
                        label: '# Maliyeti',
                        data: dataOfColunm,
                        backgroundColor: colorByMonth,
                    }]
                },

            });

            var dataSets = [];
            for (var i = 0; i < listOfLine.length; i++) {
                var temp = {
                    label: labelOfLine[i],
                    data: [],
                    backgroundColor: getRandomColor(),
                };
                for (var j = 0; j < listOfLine[i].detail.length; j++) {
                    temp.data.push(listOfLine[i].detail[j].sumAmount == null ? 0 : listOfLine[i].detail[j].sumAmount);
                }
                dataSets.push(temp);
            }
            const report = document.getElementById('reportChart');

            const reportChart = new Chart(report, {
                type: 'bar',
                data: {
                    labels: labelOfColunm,
                    datasets: dataSets,
                },

            });
        };
        $scope.Save() = function () {
            PrintImage();
            return;
        }
        function PrintImage() {
            var canvas = document.getElementById("reportChart");
            var win = window.open();
            win.document.write("<br><img src='" + canvas.toDataURL() + "'/>");
            win.print();
            win.location.reload();

        }
        function getRandomColor() {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }
        function setColorReason() {
            for (var i in $scope.lineData) {
                ict_unit.push("ICT Unit " + $scope.lineData[i].ict_unit);
                efficiency.push($scope.lineData[i].efficiency);
                colorByReason.push(getRandomColor());
            }
        }
        function setColorMonth() {
            for (var i in $scope.colunmData) {
                ict_unit.push("ICT Unit " + $scope.colunmData[i].ict_unit);
                efficiency.push($scope.colunmData[i].efficiency);
                colorByMonth.push(getRandomColor());
            }
        }
    }])
    manuelApp.controller('manualActionReasonCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {
        $scope.manualActionList = [];
        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;
        $scope.selectedItem = {};
        $scope.fetchData = function () {
            $scope.manualActionList = [];
            $http.get($rootScope.url + "/ManualActionReason/GetManager").then(function (response) {
                var i = 0;
                $scope.manualActionList = response.data.map((x) => {
                    return { ...x, id: ++i };
                });

            }, function () {

            });
        };
        $scope.sortBy = function (column) {
            $scope.sortColumn = column;
            $scope.reverse = !$scope.reverse;
        };
        $scope.Delete = function (i) {
            $http.post($rootScope.url + '/ManualActionReason/Delete/' + i.reasonID).then(function (result) {
                if (result.data) {
                    toastr.success(i.reasonName + " silindi.");
                    $scope.fetchData();
                }
            })
        };
        $scope.Insert = function () {
            if (($scope.selectedItem.reasonName != null && $scope.selectedItem.reasonName != "")) {
                $http.post($rootScope.url + '/ManualActionReason/Create', $scope.selectedItem).then(function (result) {
                    if (result != null) {
                        toastr.success($scope.selectedItem.reasonName + " eklendi.");
                        $scope.selectedItem = {};
                        $scope.fetchData();
                    }
                })
            }
            else toastr.warning("Sebep girdiğinizden emin olunuz!");
        };
        $scope.Update = function () {

            if ($scope.selectedItem.reasonName != null && $scope.selectedItem.reasonName != "") {
                $http.post($rootScope.url + '/ManualActionReason/Edit', $scope.selectedItem).then(function (result) {
                    if (result != null) {
                        toastr.success($scope.selectedItem.reasonName + " düzenlendi.");
                        $scope.selectedItem = {};
                        $scope.fetchData();
                    }
                })
            }
            else toastr.warning("Sebep girdiğinizden emin olunuz!");
        };
        $scope.UpdateData = function (i) {
            $scope.selectedItem = i;
        };
    }])
    manuelApp.controller('excelManualActionCtrl', ['$scope', '$rootScope', '$filter', '$location', '$http', function ($scope, $rootScope, $filter, $location, $http) {

        $scope.uploading = true;
        var formdata = new FormData();
        $scope.getTheFiles = function ($files) {
            angular.forEach($files, function (value, key) {
                formdata.append(key, value);
            });
        };

        // NOW UPLOAD THE FILES.
        $scope.uploadFiles = function () {
            $scope.uploading = false;
            var request = {
                method: 'POST',
                url: $rootScope.url + '/ManualAction/Upload',
                data: formdata,
                headers: {
                    'Content-Type': undefined
                }
            };


            $http(request)
                .then(function (d) {
                    console.log(d.data)
                    if (d.data) {
                        $scope.uploading = true;
                        toastr.success("Yüklendi");
                        formdata = new FormData;
                    } else {
                        $scope.uploading = true;
                        toastr.error("Doğru dosyayı yüklediğinizden emin olunuz. Tekrar deneyiniz.");
                        formdata = new FormData;
                    }

                }, function () {

                    $scope.uploading = true;

                    toastr.error("Tekrar yükleyiniz.");
                });

        }

    }])


    manuelApp.directive('ngFiles', ['$parse', function ($parse) {

        function fn_link(scope, element, attrs) {
            var onChange = $parse(attrs.ngFiles);
            element.on('change', function (event) {
                onChange(scope, { $files: event.target.files });
            });
        };

        return {
            link: fn_link
        }
    }]);
})(window.angular);