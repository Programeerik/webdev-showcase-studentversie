const inputFirstname = document.querySelector('#firstname');
const inputLastname = document.querySelector('#lastname');
const inputPhone = document.querySelector('#phone');
const inputEmail = document.querySelector('#email');
const form = document.querySelector('.form-contactpagina');
const flashMessage = document.querySelector('#flash-message');
const submitButton = document.querySelector('#submit-button');
const loader = document.querySelector("#loading");

const validateField = (input, regex, errorMsg) => {
    if (!regex.test(input.value.trim())) {
        input.setCustomValidity(errorMsg);
        input.reportValidity();
    } else {
        input.setCustomValidity("");
    }
};

inputEmail.addEventListener("input", () => validateField(inputEmail, /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/, "Ongeldig e-mailadres."));
inputPhone.addEventListener("input", () => validateField(inputPhone, /^\+?[0-9\s-]{10,20}$/, "Ongeldig telefoonnummer."));

form.addEventListener('submit', function (event) {
    event.preventDefault();

    if (!form.checkValidity()) return;

    displayLoading();
    submitButton.disabled = true;

    const formData = {
        firstname: inputFirstname.value,
        lastname: inputLastname.value,
        email: inputEmail.value,
        phone: inputPhone.value,
        __RequestVerificationToken: document.querySelector('input[name="__RequestVerificationToken"]').value
    };

    fetch('http://localhost:5001/api/Mail', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData)
    })
    .then(response => response.json())
    .then(data => {
        flashMessage.textContent = data.errors ? data.errors.join(" ") : data.message;
        flashMessage.classList.add(data.errors ? "error" : "success");
        if (!data.errors) form.reset();
    })
    .catch(() => {
        flashMessage.textContent = "Er is een fout opgetreden bij het verzenden.";
        flashMessage.classList.add("error");
    })
    .finally(() => {
        hideLoading();
        submitButton.disabled = false;
        flashMessage.textContent = "Het formulier is succesvol verzonden! Hartelijk bedankt!"
    });
});

function displayLoading() {
    loader.classList.add("display");
    setTimeout(() => {
        loader.classList.remove("display");
    }, 5000);
}

function hideLoading() {
    loader.classList.remove("display");
}