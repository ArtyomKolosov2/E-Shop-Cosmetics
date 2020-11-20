async function SetMinMaxPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMin"].value = user.min;
        form.elements["priceMax"].value = user.max;
    }
}
SetMinMaxPrice();

export function GetMinPrice() {
    return 1;
}
export function GetMaxPrice() {
    return 432;
}