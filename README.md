

    In method GetCardIssuerNameByBin - cardPrefix could be less than three digits and it may throw an unhandled exception. We        should catch an exception for this method.

    In method GetCreditCardNumbers - a random variable is used which looks like a not required variable.

    In method IsValidCreditCardNumber - an empty string is also a card number, this case should be handled.



Changes:

    Added enum CardIssuer under a namespace Constants based on the functional requirement.

    Modified the parameter list for method GenerateVisaCardNumber, random type variable is created inside the method.

    Added reference (System, System.Collections.Generic, System.Linq;) to the project for dependencies.

    Updated the parameter list for method:CreateFakeCreditCardNumber call under method: GetCreditCardNumbers. Random type variable is not required during a call it is being created inside the method: CreateFakeCreditCardNumber.
