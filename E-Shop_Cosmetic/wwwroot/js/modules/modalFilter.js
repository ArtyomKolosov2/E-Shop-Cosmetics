export function modalFilter(filterBtn) {
    function eventModalFilter() {
        modalSearch.classList.toggle("on-click");
    }
    const modalSearch = document.querySelector(".modal-search");
    const closeFilter = document.querySelector(".btn-close-filter");

    filterBtn.addEventListener("click", eventModalFilter);

    closeFilter.addEventListener("click", eventModalFilter);
}