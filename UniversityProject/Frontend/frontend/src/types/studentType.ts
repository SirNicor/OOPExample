export interface StudentServer {
    totalScore: number,
    skipHours: number,
    creditScores: number,
    countOfExamsPassed: number,
    course: number,
    personId: number,
    passport: {
        passportId: number,
        serial: number,
        number: number,
        firstName: string,
        lastName: string,
        middleName: string,
        birthData: string,
        address: {
            addressId: number,
            country: string,
            city: string,
            street: string,
            houseNumber: number
        }
    }
}

export interface StudentType {
    totalScore: number,
    skipHours: number,
    creditScores: number,
    countOfExamsPassed: number,
    course: number,
    personId: number,
    passportId: number,
    serial: number,
    number: number,
    firstName: string,
    lastName: string,
    middleName: string,
    birthData: string,
    addressId: number,
    country: string,
    city: string,
    street: string,
    houseNumber: number
}

export const mapStudentType = (studentDB: StudentServer) : any =>
{
    return{
        totalScore: studentDB.totalScore,
        skipHours: studentDB.skipHours,
        creditScores: studentDB.creditScores,
        countOfExamsPassed: studentDB.countOfExamsPassed,
        course: studentDB.course,
        personId: studentDB.personId,
        passportId: studentDB.passport.passportId,
        serial: studentDB.passport.serial,
        number: studentDB.passport.number,
        firstName: studentDB.passport.firstName,
        lastName: studentDB.passport.lastName,
        middleName: studentDB.passport.middleName,
        birthData: studentDB.passport.birthData,
        addressId: studentDB.passport.address.addressId,
        country: studentDB.passport.address.country,
        city: studentDB.passport.address.city,
        street: studentDB.passport.address.street,
        houseNumber: studentDB.passport.address.houseNumber,
    }
}