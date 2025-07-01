export interface LoginDto{
    token: AccessDto;
    userId: number;
}
export interface AccessDto{
    token: string;
    expiration: Date;
}