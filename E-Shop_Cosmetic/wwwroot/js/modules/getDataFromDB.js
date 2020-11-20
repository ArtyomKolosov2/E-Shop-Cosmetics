async function SetMinPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMin"].value = user.min;
    }
}
SetMinPrice();
async function SetMaxPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMax"].value = user.max;
    }
}
SetMaxPrice();

export function GetMinPrice() {
    return 1;
}
export function GetMaxPrice() {
    return 432;
}

