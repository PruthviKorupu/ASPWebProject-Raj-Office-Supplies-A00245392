document.addEventListener('DOMContentLoaded', function () {
    const dragContainer = document.querySelector('.drag-container');
    const gridElement = document.querySelector('.grid');
    const filterField = document.querySelector('.grid-control-field.filter-field');
    const searchField = document.querySelector('.grid-control-field.search-field');
    const sortField = document.querySelector('.grid-control-field.sort-field');
   // const addButton = document.querySelector('.grid-button.add-more-items');
    const itemTemplate = document.querySelector('.grid-item-template');
    //const characters = 'abcdefghijklmnopqrstuvwxyz';
    //const colors = ['red', 'blue', 'green'];
    const shops = $('#shops').filterMultiSelect();

    let dragOrder = [];
    let sortFieldValue;
    let searchFieldValue;

    //
    // GRID
    //

    const grid = new Muuri(gridElement, {
    /*        items: createItemElements(20),*/
        items: '*',
        showDuration: 400,
        showEasing: 'ease',
        hideDuration: 400,
        hideEasing: 'ease',
        layoutDuration: 400,
        layoutEasing: 'cubic-bezier(0.625, 0.225, 0.100, 0.890)',
        sortData: {
            price(item, element) {
                return element.getAttribute('product-price') || '';
            },
            title(item, element) {
                return element.getAttribute('product-name') || '';
            },
        },
        dragEnabled: true,
        dragHandle: '.grid-card-handle',
        dragContainer: dragContainer,
        dragRelease: {
            duration: 400,
            easing: 'cubic-bezier(0.625, 0.225, 0.100, 0.890)',
            useDragContainer: true,
        },
        dragPlaceholder: {
            enabled: true,
            createElement(item) {
                return item.getElement().cloneNode(true);
            },
        },
        dragAutoScroll: {
            targets: [window],
            sortDuringScroll: false,
            syncAfterScroll: false,
        },
    });

    window.grid = grid;

    //
    // Grid helper functions
    //

    function initDemo() {
        // Reset field values.
        searchField.value = '';
        [sortField, filterField].forEach((field) => {
            field.value = field.querySelectorAll('option')[0].value;
        });

        // Set inital search query, active filter, active sort value and active layout.
        searchFieldValue = searchField.value.toLowerCase();
        sortFieldValue = sortField.value;

        updateDragState();

        // Search field binding.
        searchField.addEventListener('keyup', function () {
            var newSearch = searchField.value.toLowerCase();
            if (searchFieldValue !== newSearch) {
                searchFieldValue = newSearch;
                filter();
            }
        });

        // Filter, sort and layout bindings.
        filterField.addEventListener('change', filter);
        sortField.addEventListener('change', sort);

        // Add/remove items bindings.
        addButton.addEventListener('click', addItems);
        gridElement.addEventListener('click', (e) => {
            if (e.target.matches('.grid-card-remove')) {
                removeItem(grid.getItem(e.target.closest('.grid-item')));
            }
        });
    }

    function filter(onFinish = null) {
        const filterFieldValue = filterField.value;
        grid.filter(
            (item) => {
                const element = item.getElement();
                const isSearchMatch =
                    !searchFieldValue ||
                    (element.getAttribute('product-name') || '').toLowerCase().indexOf(searchFieldValue) > -1;
                const isFilterMatch =
                    !filterFieldValue || filterFieldValue === element.getAttribute('product-price');
                return isSearchMatch && isFilterMatch;
            },
            { onFinish: onFinish }
        );
    }

    function sort() {
        var currentSort = sortField.value;
        if (sortFieldValue === currentSort) return;

        updateDragState();

        // If we are changing from "order" sorting to something else
        // let's store the drag order.
        if (sortFieldValue === 'order') {
            dragOrder = grid.getItems();
        }

        // Sort the items.
        grid.sort(
            currentSort === 'title' ? 'title' : currentSort === 'price' ? 'price title' : dragOrder
        );

        // Update active sort value.
        sortFieldValue = currentSort;
    }

    // Main function
    function sort_byint() {
        var $wrap = $('.grid');
        $wrap.find('.item').sort(function (a, b) {
            return + a.getAttribute('product-price') -
                +b.getAttribute('product-price');
        })
            .appendTo($wrap);
    }

    let addEffectTimeout = null;
    function addItems() {
        addButton.classList.add('processing');

        const items = grid.add(createItemElements(5), {
            layout: false,
            active: false,
        });

        if (sortFieldValue !== 'order') {
            grid.sort(sortFieldValue === 'title' ? 'title' : 'color title', {
                layout: false,
            });
            dragOrder = dragOrder.concat(items);
        }

        filter();

        if (addEffectTimeout) clearTimeout(addEffectTimeout);
        addEffectTimeout = setTimeout(() => {
            addEffectTimeout = null;
            addButton.classList.remove('processing');
        }, 250);
    }

    function removeItem(item) {
        if (!item) return;

        grid.hide([item], {
            onFinish: () => {
                grid.remove(item, { removeElements: true });
                if (sortFieldValue !== 'order') {
                    const itemIndex = dragOrder.indexOf(item);
                    if (itemIndex > -1) dragOrder.splice(itemIndex, 1);
                }
            },
        });
    }

    function updateDragState() {
        if (sortField.value === 'order') {
            gridElement.classList.add('drag-enabled');
        } else {
            gridElement.classList.remove('drag-enabled');
        }
    }

    //
    // Generic helper functions
    //

    function createItemElements(amount) {
        const elements = [];
        for (let i = 0; i < amount; i++) {
            elements.push(createItemElement());
        }
        return elements;
    }

    function createItemElement() {
        const title = getRandomWord(2);
        const color = getRandomItem(colors);
        const width = getRandomInt(1, 2);
        const height = getRandomInt(1, 2);
        const itemElem = document.importNode(itemTemplate.content.children[0], true);

        itemElem.classList.add('h' + height, 'w' + width, color);
        itemElem.setAttribute('data-color', color);
        itemElem.setAttribute('data-title', title);
        itemElem.querySelector('.grid-card-title').innerHTML = title;

        return itemElem;
    }

    function getRandomItem(collection) {
        return collection[Math.floor(Math.random() * collection.length)];
    }

    // https://stackoverflow.com/a/7228322
    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    function getRandomWord(length) {
        var ret = '';
        for (var i = 0; i < length; i++) {
            ret += getRandomItem(characters);
        }
        return ret;
    }

    //
    // Fire it up!
    //

    initDemo();
});
