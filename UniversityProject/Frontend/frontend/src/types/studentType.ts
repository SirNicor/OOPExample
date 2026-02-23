export interface Filter
{
    FilterBirthDayStart?: Date,
    FilterBirthDayEnd?: Date,
    FilterSkipHoursStart?: number,
    FilterSkipHoursEnd?: number,
    FilterCourse?: number,
    FilterTotalScore?: number,
}

export interface StudentsType {
    studentId: number,
    fio: string,
    dob: string,
    address: string,
    serial: number,
    number: number,
    totalScore: number,
    skipHours: number,
    creditScores: number,
    course: number
}

export interface StudentsTypeForPage {
    studentId: number,
    passportId: number,
    addressId: number,
    criminalRecord: boolean,
    totalScore: number,
    skipHours: number,
    creditScores: number,
    countOfExamsPassed: number,
    course: number,
    chatId: number,
    firstName: string,
    lastName: string,
    middleName: string,
    dob: Date,
    country: string,
    city: string,
    state: string,
    houseNumber: string,
    serial: string,
    number: string,
    placeReceipt: string
}

