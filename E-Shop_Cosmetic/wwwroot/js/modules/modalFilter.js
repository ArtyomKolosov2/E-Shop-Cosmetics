// modal filter
export function modalFilter(filterBtn) {
    function eventModalFilter() {
        modalSearch.classList.toggle("on-click");
    }
    const modalSearch = document.querySelector(".modal-search");
    const closeFilter = document.querySelector(".btn-close-filter");
    const cancelFilter = document.querySelector(".btn-cancel-filter");

    filterBtn.addEventListener("click", eventModalFilter);

    closeFilter.addEventListener("click", eventModalFilter);

    cancelFilter.addEventListener("click", eventModalFilter);
}