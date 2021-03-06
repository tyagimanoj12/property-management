export class LoginResultModel {
    accountId: string;
    token: string;
    expireAt: Date;
    administratorId: string;
    practiceEmail: string;
    practiceName: string;
    ownerId: string;
}

export class ChangePasswordResponse {
    success: boolean;
    message: string;
}