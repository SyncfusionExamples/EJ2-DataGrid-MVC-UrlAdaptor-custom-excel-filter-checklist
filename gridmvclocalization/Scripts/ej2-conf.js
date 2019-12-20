ej.base.enableRipple(true)
var L10n = ej.base.L10n;
L10n.load({
    'ru-RU': {
        'grid': {
            'EmptyRecord': 'Нет данных для отображения',
            'GroupDropArea': 'Перетащите заголовок столбца для группировки по нему',
            'UnGroup': 'Нажмите здесь, чтобы отменить группировку',
            'EmptyDataSourceError': 'DataSource не должен быть пустым при начальной загрузке, поскольку столбцы генерируются из dataSource в сетке столбцов AutoGenerate',
            'Item': 'запись',
            'Items': 'записей',
            'Add': 'Добавить',
            'Edit': 'Измнить',
            'Delete': 'Удалить',
            'Update': 'Сохранить',
            'Cancel': 'Отмена',
            'Print': 'Печать',
            'Excelexport': 'Экспорт в Excel',
            'Equal': "Равно",
            'NotEqual': 'Не равно',
            'StartsWith': 'Начинается',
            'EndsWith': 'Заканчивается',
            'Contains': 'Содержит',
            'EnterValue': 'Введите значение',
            'FilterButton': 'Применить',
            'ClearButton': 'Отменить',  
        },
        'pager': {
            'currentPageInfo': '{0} из {1} страниц',
            'totalItemsInfo': '({0} записей)',
            'firstPageTooltip': 'Первая страница',
            'lastPageTooltip': 'Последняя страница',
            'nextPageTooltip': 'Следующая страница',
            'previousPageTooltip': 'Предыдущая',
            'nextPagerTooltip': 'Следующие страницы',
            'previousPagerTooltip': 'Предыдущие страницы'
        }
    }
});

loadCultureFiles('ru');

ej.base.setCulture('ru');

function loadCultureFiles(name) {
    var files = ['ca-gregorian.json', 'numbers.json', 'timeZoneNames.json'];
    if (name === 'ar') {
        files.push('numberingSystems.json');
    }
    var loader = ej.base.loadCldr;
    var loadCulture = function (prop) {
        var val, ajax;
        if (name === 'ar' && prop === files.length - 1) {
            ajax = new ej.base.Ajax(location.origin + /*location.pathname + */'/Scripts/cldr/supplemental/' + files[prop], 'GET', false);
        } else {
            ajax = new ej.base.Ajax(location.origin + /*location.pathname + */'/Scripts/cldr/main/' + name + '/' + files[prop], 'GET', false);
        }
        ajax.onSuccess = function (value) {
            val = value;
        };
        ajax.send();
        loader(JSON.parse(val));
    };
    for (var prop = 0; prop < files.length; prop++) {
        loadCulture(prop);
    }
}