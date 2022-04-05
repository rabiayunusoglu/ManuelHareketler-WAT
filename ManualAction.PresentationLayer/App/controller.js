
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
        $scope.itemsPerPage = 200;
        $scope.toogleForFinishButton = true;
        $scope.currentPage2 = 1;
        $scope.itemsPerPage2 = 200;
        $scope.currentPage3 = 1;
        $scope.itemsPerPage3 = 200;
        $scope.currentPage4 = 1;
        $scope.itemsPerPage4 = 200;
        $scope.departmantList = [];
        $scope.userList = [];
        $scope.reasonList = [];
        $scope.storedList = [];
        $scope.startDate = new Date().toISOString().substr(0, 10);
        $scope.endDate = new Date().toISOString().substr(0, 10);
        $scope.chosefilterAction = true;

        $scope.fetchData = function () {
            $scope.uploading = false;
            $scope.manualActionList = [];
            $http.get($rootScope.url + "/ManualAction/GetManagerByRegNo").then(function (response) {
                if (response != null || response.data != null) {
                    var i = 0; var j = 0; var k = 0; var l = 0; var m = 0;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i, _checked: false };
                    });
                    $scope.storedList = $scope.manualActionList;
                    $scope.notFinished = $scope.manualActionList.filter(t => t.statusType != 3);
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j, _checked: false };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k, _checked: false };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                    $scope.totalCount = $scope.noStartedList.length + $scope.startedList.length;
                    $scope.totalCountStartedList = $scope.startedList.length;
                    $scope.totalCountNoStartedList = $scope.noStartedList.length;
                }
                $scope.uploading = true;
                $scope.$watch('searchText', function (term) {
                    $scope.filterList = $filter('filter')($scope.manualActionList, term);
                    $scope.calculateCounts();
                });
                $scope.$watch('searchText3', function (term) {
                    $scope.filterListStartedList = $filter('filter')($scope.startedList, term);
                    $scope.calculateCountsStartedList();
                });
                $scope.$watch('searchText2', function (term) {
                    $scope.filterListNoStartedList = $filter('filter')($scope.noStartedList, term);
                    $scope.calculateCountsNoStartedList();
                });
            }, function () {
                $scope.uploading = true;
            });
            $scope.toogleForFinishButton = true;

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
                        return { ...x, id: ++i, _checked: false };
                    });
                    $scope.storedList = $scope.manualActionList;
                    $scope.notFinished = $scope.manualActionList.filter(t => t.statusType != 3);
                    $scope.noStartedList = $scope.manualActionList.filter(x => x.statusType == 1).map((x) => {
                        return { ...x, id: ++j, _checked: false };
                    });
                    $scope.startedList = $scope.manualActionList.filter(x => x.statusType == 2).map((x) => {
                        return { ...x, id: ++k, _checked: false };
                    });
                    $scope.finishedList = $scope.manualActionList.filter(x => x.statusType == 3).map((x) => {
                        return { ...x, id: ++l };
                    });
                    $scope.totalCount = $scope.noStartedList.length + $scope.startedList.length;
                    $scope.totalCountStartedList = $scope.startedList.length;
                    $scope.totalCountNoStartedList = $scope.noStartedList.length;
                }
                $scope.uploading = true;
                $scope.$watch('searchText', function (term) {
                    $scope.filterList = $filter('filter')($scope.manualActionList, term);
                    $scope.calculateCounts();
                });
                $scope.$watch('searchText3', function (term) {
                    $scope.filterListStartedList = $filter('filter')($scope.startedList, term);
                    console.log($scope.filterListStartedList)
                    $scope.calculateCountsStartedList();
                });
                $scope.$watch('searchText2', function (term) {
                    $scope.filterListNoStartedList = $filter('filter')($scope.noStartedList, term);
                    console.log($scope.filterListNoStartedList)
                    $scope.calculateCountsNoStartedList();
                });
            }, function () {
                $scope.uploading = true;
            });
            $scope.toogleForFinishButton = true;
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
                    $scope.fetchData();
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
            }
            $scope.uploading = false;
            item.userRegisterNo = item.redirectedUser.registerNo;
            item.username = item.redirectedUser.username;
            item.redirectedUser = item.redirectedUser.username;
            $http.post($rootScope.url + "/ManualAction/RedirectItem", item).then(function (response) {
                if (response != null || response.data != false) {
                    $scope.fetchData();
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
                    url: $rootScope.url + '/ManualAction/ExportByRegNo',
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
                    url: $rootScope.url + `/ManualAction/ExportByDateRegNo?startDate=${$scope.startDate}&endDate=${$scope.endDate}`,
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

        //calculate count 
        $scope.calculateCounts = function () {
            $scope.selectableTotalCount = $scope.filterList.filter(x => x.statusType != 3).length;
            $scope.selectedCount = $scope.manualActionList.filter(x => x.statusType != 3 && x._checked == true).length;
        };
        $scope.calculateCountsStartedList = function () {
            $scope.selectableTotalCountStartedList = $scope.filterListStartedList.length;
            $scope.selectedCountStartedList = $scope.startedList.filter(x => x._checked == true).length;
        };
        $scope.calculateCountsNoStartedList = function () {
            $scope.selectableTotalCountNoStartedList = $scope.filterListNoStartedList.length;
            $scope.selectedCountNoStartedList= $scope.noStartedList.filter(x => x._checked == true).length;
        };

        //set checkboxclick Total
        $scope.setCheckBox = function (item, value) {
            var foundIndex = $scope.findItemIndex(item);
            $scope.manualActionList[foundIndex]._checked = value;
        };
        $scope.setCheckBoxByItem = function (item) {
            item._checked = !item._checked;
            $scope.calculateCounts();
        };
        $scope.findItemIndex = function (item) {
            return $scope.manualActionList.findIndex((i) => i.manualActionID === item.manualActionID);
        };
        $scope.chosenFilterActions = function () {
            if ($scope.manualActionList.length !== 0) {
                $scope.uploading = false;
                if ($scope.chosefilterAction)
                    $scope.filterList.map(x => $scope.setCheckBox(x, true))
                else
                    $scope.filterList.map(x => $scope.setCheckBox(x, false))
                $scope.chosefilterAction = !$scope.chosefilterAction;
                $scope.calculateCounts();
            }
            $scope.uploading = true;
        };

        //set checkboxclick No Started
        $scope.setCheckBoxNoStarted = function (item, value) {
            var foundIndex = $scope.findItemIndexNoStarted(item);
            $scope.noStartedList[foundIndex]._checked = value;
        };
        $scope.setCheckBoxByItemNoStarted = function (item) {
            item._checked = !item._checked;
            $scope.calculateCountsNoStartedList();
        };
        $scope.findItemIndexNoStarted = function (item) {
            return $scope.noStartedList.findIndex((i) => i.manualActionID === item.manualActionID);
        };
        $scope.chosenFilterActionsNoStarted = function () {
            if ($scope.noStartedList.length !== 0) {
                $scope.uploading = false;
                if ($scope.chosefilterAction)
                    $scope.filterListNoStartedList.map(x => $scope.setCheckBoxNoStarted(x, true))
                else
                    $scope.filterListNoStartedList.map(x => $scope.setCheckBoxNoStarted(x, false))
                $scope.chosefilterAction = !$scope.chosefilterAction;
                $scope.calculateCountsNoStartedList();
            }
            $scope.uploading = true;
        };

        //set checkboxclick Started
        $scope.setCheckBoxStarted = function (item, value) {
            var foundIndex = $scope.findItemIndexStarted(item);
            $scope.startedList[foundIndex]._checked = value;
        };
        $scope.setCheckBoxByItemStarted = function (item) {
            item._checked = !item._checked;
            $scope.calculateCountsStartedList();
        };
        $scope.findItemIndexStarted = function (item) {
            return $scope.startedList.findIndex((i) => i.manualActionID === item.manualActionID);
        };
        $scope.chosenFilterActionsStarted = function () {
            if ($scope.startedList.length !== 0) {
                $scope.uploading = false;
                if ($scope.chosefilterAction)
                    $scope.filterListStartedList.map(x => $scope.setCheckBoxStarted(x, true))
                else
                    $scope.filterListStartedList.map(x => $scope.setCheckBoxStarted(x, false))
                $scope.chosefilterAction = !$scope.chosefilterAction;
                $scope.calculateCountsStartedList();
            }
            $scope.uploading = true;
        };

        //redirect and complete actions 
        $scope.TotalRedirected = async function (item, type) {
            var list = [];
            if (type == 0)
                list =$scope.manualActionList.filter(x => x.statusType != 3 && x._checked == true);
            else if (type == 1)
                list =$scope.noStartedList.filter(x => x._checked == true);
            else if (type == 2)
                list = $scope.startedList.filter(x => x._checked == true);
            console.log(list);
            console.log(type);
            if (list == null || list.length == 0) {
                toastr.warning("Seçim yapınız.")
                return;
            }
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
            }
            list.forEach(x => x.userRegisterNo = item.redirectedUser.registerNo);
            list.forEach(x => x.username = item.redirectedUser.username);
            list.forEach(x => x.redirectedUser = item.redirectedUser.username);
            list.forEach(x => x.redirectedTeam = item.redirectedTeam);
            list.forEach(x => x.redirectedText = item.redirectedText);
            $scope.uploading = false;

            for (var i = 0; i < list.length; i++) {
                list[i].userRegisterNo = item.redirectedUser.registerNo;
                list[i].username = item.redirectedUser.username;
                list[i].redirectedUser = item.redirectedUser.username;
                list[i].redirectedTeam = item.redirectedTeam;
                list[i].redirectedText = item.redirectedText;
                await $http.post($rootScope.url + "/ManualAction/RedirectItem", list[i]).then(function (response) {
                    if (response != null || response.data != false) {
                        if (i == list.length - 1) {
                            toastr.success("Yönlendirildi");
                            $scope.uploading = true;
                            $scope.redirectItem = [];
                            $scope.fetchData();
                        }
                    }
                }, function () {
                    toastr.error("Bir sorun oluştu");
                    $scope.uploading = true;
                    $scope.redirectItem = [];
                });
            }

        }
        $scope.TotalFinished = async function (type) {
            if (type == 0)
                var list = $scope.manualActionList.filter(x => x.statusType != 3 && x._checked == true);
            else if (type == 1)
                var list = $scope.noStartedList.filter(x => x._checked == true);
            else if (type == 2)
                var list = $scope.startedList.filter(x => x._checked == true);
            if (list == null || list.length == 0) {
                toastr.warning("Seçim yapınız.")
                return;
            }
            if ($scope.finishItem.actionReason == null || $scope.finishItem.actionReason.length == 0) {
                toastr.warning("Sebep seçmelisiniz");
                return;
            }
            if ($scope.finishItem.reasonDetail == null || $scope.finishItem.reasonDetail.length == 0) {
                toastr.warning("Açıklama girmelisiniz");
                return;
            }
            for (var i = 0; i < list.length; i++) {
                if (i == 0)
                    $scope.uploading = false;
                list[i].statusType = 3;
                list[i].statusName = "Tamamlandı";
                list[i].madeDate = new Date();
                list[i].actionReason = $scope.finishItem.actionReason;
                list[i].reasonDetail = $scope.finishItem.reasonDetail;
                await $http.post($rootScope.url + "/ManualAction/Edit", list[i]).then(function (response) {
                    if (response != null || response.data != false) {
                        if (i == list.length - 1) {
                            $scope.uploading = true;
                            toastr.success("Tamamlandı");
                            $scope.finishItem = [];
                            $scope.fetchData();
                        }
                    }
                }, function () {
                    toastr.error("Bir sorun oluştu");
                    $scope.uploading = true;
                    $scope.finishItem = [];
                });
            }
        };

        $scope.ToggleFinishButton = function () {
            $scope.manualActionList = [];
            if (!$scope.toogleForFinishButton) {
                $scope.manualActionList = $scope.storedList;
                $scope.filterList = $scope.manualActionList;
            }
            else {
                $scope.manualActionList = $scope.notFinished;
                $scope.filterList = $scope.manualActionList;
            }
            $scope.toogleForFinishButton = !$scope.toogleForFinishButton
        }
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
        $scope.toogleForFinishButton = false;
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
                    var i = 0; var j = 0; var k = 0; var l = 0; var m = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.storedList = $scope.manualActionList;
                    $scope.notFinished = $scope.manualActionList.filter(t => t.statusType != 3);

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
            $scope.toogleForFinishButton = true;
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
                    var i = 0; var j = 0; var k = 0; var l = 0; var m = 0;
                    $scope.manualActionList = response.data;
                    $scope.manualActionList = response.data.map((x) => {
                        return { ...x, id: ++i };
                    });
                    $scope.storedList = $scope.manualActionList;
                    $scope.notFinished = $scope.manualActionList.filter(t => t.statusType != 3);

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
            $scope.toogleForFinishButton = true;
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

        $scope.ToggleFinishButton = function () {
            $scope.manualActionList = [];
            if (!$scope.toogleForFinishButton)
                $scope.manualActionList = $scope.storedList;
            else
                $scope.manualActionList = $scope.notFinished;
            $scope.toogleForFinishButton = !$scope.toogleForFinishButton
            console.log($scope.manualActionList.length);
        }
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
                    $scope.year = response.data[3];
                    if (response.data[0] == null || response.data[0].length == 0) {
                        $scope.uploading = true;
                        return;
                    }
                    $scope.columnList = response.data[0].map(x => {
                        return {
                            ...x, month: (x.sortingDate.substr(4, 2)), year: (x.sortingDate.substr(0, 4)),
                            colName: ((x.sortingDate.substr(4, 2)) == "01") ? "Ocak" : ((x.sortingDate.substr(4, 2)) == "02") ? "Şubat" : ((x.sortingDate.substr(4, 2)) == "03") ? "Mart" : ((x.sortingDate.substr(4, 2)) == "04") ? "Nisan" : ((x.sortingDate.substr(4, 2)) == "05") ? "Mayıs" : ((x.sortingDate.substr(4, 2)) == "06") ? "Haziran" : ((x.sortingDate.substr(4, 2)) == "07") ? "Temmuz" : ((x.sortingDate.substr(4, 2)) == "08") ? "Ağustos" : ((x.sortingDate.substr(4, 2)) == "09") ? "Eylül" : ((x.sortingDate.substr(4, 2)) == "10") ? "Ekim" : ((x.sortingDate.substr(4, 2)) == "11") ? "Kasım" : ((x.sortingDate.substr(4, 2)) == "12") ? "Aralık" : "",
                        }
                    });
                    $scope.selectBar = $scope.columnList;

                    $scope.colunmLabel = $scope.columnList.map(t => t.colName + "-" + t.year);
                    $scope.colunmData = $scope.columnList.map(t => t.sumAmountInverse);

                    $scope.lineList = response.data[1];
                    $scope.lineLabel = $scope.lineList.map(t => t.actionReason == "-" ? "Sebep Girilmemiş" : t.actionReason);
                    $scope.lineData = $scope.lineList.map(t => t.sumAmountInverse);

                    $scope.setChart($scope.lineLabel, $scope.lineData, $scope.lineList, $scope.colunmLabel, $scope.colunmData);
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

        $scope.fetchDataByDate = function () {
            $scope.uploading = false;
            $scope.manualActionList = [];
            var date = (($scope.selectedMonth) == "Ocak") ? "01" : (($scope.selectedMonth) == "Şubat") ? "02" : (($scope.selectedMonth) == "Mart") ? "03" : (($scope.selectedMonth) == "Nisan") ? "04" : (($scope.selectedMonth) == "Mayıs") ? "05" : (($scope.selectedMonth) == "Haziran") ? "06" : (($scope.selectedMonth) == "Temmuz") ? "07" : (($scope.selectedMonth) == "Ağustos") ? "08" : (($scope.selectedMonth) == "Eylül") ? "09" : (($scope.selectedMonth) == "Ekim") ? "10" : (($scope.selectedMonth) == "Kasım") ? "11" : (($scope.selectedMonth) == "Aralık") ? "12" : "-";
            $http.get($rootScope.url + "/ManualAction/GetReportByDate?sortDate=" + $scope.selectedYear.productYear + date).then(function (response) {
                if (response != null || response.data != null) {
                    console.log(response.data)

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
                    $scope.year = response.data[3];

                    $scope.colunmLabel = $scope.columnList.map(t => t.colName + "-" + t.year);
                    $scope.colunmData = $scope.columnList.map(t => t.sumAmountInverse);

                    $scope.lineList = response.data[1];
                    $scope.lineLabel = $scope.lineList.map(t => t.actionReason == "-" ? "Sebep Girilmemiş" : t.actionReason);
                    $scope.lineData = $scope.lineList.map(t => t.sumAmountInverse);
                    $scope.monthBarChart.destroy();
                    $scope.reportChart.destroy();
                    $scope.monthPieChart.destroy();
                    $scope.reasonBarChart.destroy();
                    $scope.reasonPieChart.destroy();
                    $scope.setChart($scope.lineLabel, $scope.lineData, $scope.lineList, $scope.colunmLabel, $scope.colunmData);
                }
                $scope.uploading = true;
            }, function () {
                $scope.uploading = true;
            });
        };

        $scope.setChart = function (labelOfLine, dataOfLine, listOfLine, labelOfColunm, dataOfColunm) {
            setColorReason();
            setColorMonth();
            const reasonPie = document.getElementById('reasonPieChart');
            $scope.reasonPieChart = new Chart(reasonPie, {
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
            $scope.reasonBarChart = new Chart(reasonBar, {
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
            $scope.monthPieChart = new Chart(monthPie, {
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
            $scope.monthBarChart = new Chart(monthBar, {
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
                    temp.data.push(listOfLine[i].detail[j].sumAmount == null ? 0 : listOfLine[i].detail[j].sumAmountInverse);
                }
                dataSets.push(temp);
            }
            const report = document.getElementById('reportChart');
            $scope.reportChart = new Chart(report, {
                type: 'bar',
                data: {
                    labels: labelOfColunm,
                    datasets: dataSets,
                },

            });

        };

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
        $scope.newReasonName = "";
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
            if (($scope.newReasonName != null && $scope.newReasonName != "")) {
                $scope.selectedItem.reasonName = $scope.newReasonName;
                $http.post($rootScope.url + '/ManualActionReason/Create', $scope.selectedItem).then(function (result) {
                    if (result != null) {
                        toastr.success($scope.selectedItem.reasonName + " eklendi.");
                        $scope.newReasonName = "";
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
        $scope.uploading1 = true;
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

        $scope.fetchData = function () {
            $scope.uploading1 = false;
            $http.get($rootScope.url + "/ManualAction/GetYear").then(function (response) {
                if (response != null || response.data != null) {
                    $scope.year = response.data;
                }
                $scope.uploading1 = true;
            }, function () {
                $scope.uploading1 = true;
            });
        };
        $scope.deleteData = function () {
            $scope.uploading1 = false;
            var date = (($scope.selectedMonth) == "Ocak") ? "01" : (($scope.selectedMonth) == "Şubat") ? "02" : (($scope.selectedMonth) == "Mart") ? "03" : (($scope.selectedMonth) == "Nisan") ? "04" : (($scope.selectedMonth) == "Mayıs") ? "05" : (($scope.selectedMonth) == "Haziran") ? "06" : (($scope.selectedMonth) == "Temmuz") ? "07" : (($scope.selectedMonth) == "Ağustos") ? "08" : (($scope.selectedMonth) == "Eylül") ? "09" : (($scope.selectedMonth) == "Ekim") ? "10" : (($scope.selectedMonth) == "Kasım") ? "11" : (($scope.selectedMonth) == "Aralık") ? "12" : "-";

            console.log($scope.selectedYear.productYear + date)
            $http.post($rootScope.url + `/ManualAction/DeleteByDate?sortDate=${$scope.selectedYear.productYear + date}`).then(function (response) {
                if (response != null || response.data != null) {
                    if (response.data)
                        toastr.success("Silindi");
                    else
                        toastr.error("Başarısız");
                }
                $scope.uploading1 = true;
            }, function () {
                $scope.uploading1 = true;
            });
        };
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