export async function GetMinPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMin"].value = user.min;
        return ;
    } else {
        return 0;
    }
}
export async function GetMaxPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMax"].value = user.max;
        return ;
    } else {
        return 0;
    }
}