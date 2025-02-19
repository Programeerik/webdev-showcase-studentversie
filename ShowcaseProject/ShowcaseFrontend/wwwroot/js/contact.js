document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector('.form-contactpagina');
    const flashMessage = document.querySelector('#flash-message');
    const submitButton = document.querySelector('#submit-button');
    const loader = document.querySelector("#loading");

    form.addEventListener('submit', async function (event) {
        event.preventDefault();

        if (!form.checkValidity()) return;

        displayLoading();
        submitButton.disabled = true;
        clearErrors();

        const formData = {
            FirstName: form.firstname.value,
            LastName: form.lastname.value,
            Email: form.email.value,
            Phone: form.phone.value,
            Subject: form.subject.value,
            Message: form.message.value,
            RecaptchaResponse: recaptchaToken
        };

        try {
            const response = await fetch('http://localhost:5001/api/mail', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });

            const data = await response.json();

            if (!response.ok) {
                displayErrors(data.errors || {});
                flashMessage.textContent = data.message || "Er is een fout opgetreden.";
                flashMessage.classList.add("error");
            } else {
                flashMessage.textContent = "Het formulier is succesvol verzonden! Hartelijk bedankt!";
                flashMessage.classList.add("success");
                form.reset();
                grecaptcha.reset();
            }
        } catch (error) {
            flashMessage.textContent = "Er is een fout opgetreden bij het verzenden.";
            flashMessage.classList.add("error");
        } finally {
            hideLoading();
            submitButton.disabled = false;
        }
    });

    function displayErrors(errors) {
        Object.keys(errors).forEach(field => {
            const inputField = form.querySelector(`[name=${field.toLowerCase()}]`);
            if (inputField) {
                const errorElement = document.createElement("span");
                errorElement.classList.add("text-danger");
                errorElement.id = `error-${field}`;
                errorElement.textContent = errors[field].join(", ");

                inputField.parentElement.appendChild(errorElement);
            }
        });
    }

    function clearErrors() {
        document.querySelectorAll(".text-danger").forEach(el => el.remove());
        flashMessage.textContent = "";
        flashMessage.classList.remove("error", "success");
    }

    function displayLoading() {
        loader.classList.add("display");
    }

    function hideLoading() {
        loader.classList.remove("display");
    }
});
