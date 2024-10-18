// js/script.js

const baseUrl = 'http://localhost:5268';

document.querySelector('#frmGenerate').addEventListener('submit', (e) => {
    e.preventDefault();

    
    let endpoint = '/api/PersonalData';
    if (e.target.chkPerson.checked) {
        endpoint += '/persons';  // This is correct
        const numPersons = parseInt(e.target.txtNumberPersons.value);
        if (numPersons > 1) {
            endpoint += '/' + numPersons;  // Corrected: Append the number properly
        }
} else {
        const selectedOption = document.querySelector('input[name="fake-data"]:checked')?.id;
        endpoint += '/' + document.getElementById('cmbPartialOptions').value;
    }

    // API call
    fetch(baseUrl + endpoint)
        .then(response => {
            if (!response.ok) {
                handleError();
            } else {
                return response.json();
            }
        })
        .then(handlePersonData)
        .catch(handleError);
});

const handlePersonData = (data) => {
    const output = document.querySelector('#output');
    output.innerHTML = '';

    // Handle both single and bulk data (if the data is not an array, make it an array)
    if (!Array.isArray(data)) {
        data = [data];
    }

    data.forEach(item => {
        const personCard = document.importNode(document.getElementById('personTemplate').content, true);

        if (item.cpr !== undefined) {
            const cprValue = personCard.querySelector('.cprValue');
            cprValue.innerText = item.cpr;
            cprValue.classList.remove('hidden');
            personCard.querySelector('.cpr').classList.remove('hidden');
        }
        if (item.firstName !== undefined) {
            const firstNameValue = personCard.querySelector('.firstNameValue');
            firstNameValue.innerText = item.firstName;
            firstNameValue.classList.remove('hidden');
            const lastNameValue = personCard.querySelector('.lastNameValue');
            lastNameValue.innerText = item.surName;  // Updated to match your C# model
            lastNameValue.classList.remove('hidden');
            personCard.querySelector('.firstName').classList.remove('hidden');
            personCard.querySelector('.lastName').classList.remove('hidden');
        }
        if (item.gender !== undefined) {
            const genderValue = personCard.querySelector('.genderValue');
            genderValue.innerText = item.gender;
            genderValue.classList.remove('hidden');
            personCard.querySelector('.gender').classList.remove('hidden');
        }
        if (item.dateOfBirth !== undefined) {
            const dobValue = personCard.querySelector('.dobValue');
            dobValue.innerText = item.dateOfBirth;  // Ensure it's formatted properly
            dobValue.classList.remove('hidden');
            personCard.querySelector('.dob').classList.remove('hidden');
        }
        if (item.streetName !== undefined && item.streetNumber !== undefined) {
            const streetValue = personCard.querySelector('.streetValue');
            streetValue.innerText = `${item.streetName} ${item.streetNumber}, ${item.floor || ''}.${item.door || ''}`;
            streetValue.classList.remove('hidden');
            const townValue = personCard.querySelector('.townValue');
            townValue.innerText = `${item.zipCode} ${item.town}`;
            townValue.classList.remove('hidden');
            personCard.querySelector('.address').classList.remove('hidden');
        }
        if (item.phoneNumber !== undefined) {
            const phoneNumberValue = personCard.querySelector('.phoneNumberValue');
            phoneNumberValue.innerText = item.phoneNumber;
            phoneNumberValue.classList.remove('hidden');
            personCard.querySelector('.phoneNumber').classList.remove('hidden');
        }

        output.appendChild(personCard);
    });
    output.classList.remove('hidden');
};

const handleError = () => {
    const output = document.querySelector('#output');

    output.innerHTML =
        '<p>There was a problem communicating with the API</p>';
    output.classList.add('error');

    setTimeout(() => {
        output.innerHTML = '';
        output.classList.remove('error');
    }, 2000);
};
